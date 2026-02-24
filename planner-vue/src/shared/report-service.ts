import { ref, onMounted } from "vue";
import api from "../shared/api";

export function LoadReport() {
  const url = `Planner?Level=2`;
  const reportData = ref([]);
  const metaData = ref<object | null>(null);
  const isLoading = ref(true);
  const error = ref<string | null>(null);

  const getReport = async () => {
    isLoading.value = true;
    try {
      const responce = await api.get(url);
      metaData.value = responce.data.metaData;
      reportData.value = responce.data.data;
    } catch (err) {
      error.value = "Ошибка при загрузке отчета";
      console.error(err);
    } finally {
      isLoading.value = false;
    }
  };

  onMounted(() => getReport());

  return { reportData, metaData, isLoading, error };
}

export const flattenRows = (items, rowDefs, colDefs) => {
  const result = [];
  const itemRows = [];
  const walk = (dataItems, level = 0) => {
    dataItems.forEach((item) => {
      const childrenKey = Object.keys(item).find(
        (key) => Array.isArray(item[key]) && item[key].length > 0,
      );

      if (childrenKey) {
        walk(item[childrenKey], level + 1);
      }
      itemRows.push(...getUnitsRows(rowDefs, colDefs, item, level));
      if (level == 0) {
        itemRows[0].sku = item.sku;
        result.push(...itemRows);
        itemRows.length = 0;
      }
    });
  };

  walk(items);
  return result;
};

export const getTotal = (data, rowDefs, colDefs) => {
  return getUnitsRows(rowDefs, colDefs, data, 1, "Total", "Всего");
};

const getUnitsRows = (
  rowDefs,
  colDefs,
  item,
  level,
  sku = null,
  skuSub = null,
) => {
  const result = [];
  let lastSku;
  let lastSkuSub;
  rowDefs.forEach((def) => {
    const rowPath = def.path.toLowerCase();
    const contributionGrowth = item?.contributionGrowth[def.label] ?? 0;
    const skuName = sku ? sku : "";
    const skuSubName = skuSub
      ? skuSub
      : level === 0
        ? "Итого Sku"
        : level === 1
          ? (item.skuSubName ?? "")
          : "";
    const rowObject = {
      sku: level != 1 || skuName === lastSku ? "" : skuName,
      skuSubName: skuSubName === lastSkuSub ? "" : skuSubName,
      rowTitle: def.label,
      contributionGrowth: (contributionGrowth * 100).toFixed(1) + "%",
      _level: level,
    };
    if (level == 1) lastSku = skuName;
    lastSkuSub = skuSubName;

    colDefs.forEach((col) => {
      if (
        ["sku", "skusubname", "rowtitle", "contributiongrowth"].includes(
          col.key.toLowerCase(),
        )
      )
        return;

      const value = findValueInItem(item, col.key, rowPath);
      rowObject[col.key] = value !== undefined ? value : null;
    });

    result.push(rowObject);
  });
  return result;
};

const findValueInItem = (item, colKey, rowKey) => {
  const search = (obj) => {
    if (!obj || typeof obj !== "object") return null;

    if (obj[colKey] && typeof obj[colKey] === "object") {
      return obj[colKey][rowKey];
    }

    for (const k in obj) {
      if (Array.isArray(obj[k])) continue;
      const found = search(obj[k]);
      if (found !== null && found !== undefined) return found;
    }
    return null;
  };

  return search(item);
};
