<script setup>
import { computed } from 'vue';
import { flattenRows, getTotal } from '../shared/report-service' // Импортируем вашу функцию
import { ref } from "vue";

const props = defineProps({
  reportData: {
    type: Object,
    required: true
  },
  metaData: {
    type: Object,
    required: true
  }
});

const editingCell = ref({ rowIndex: null, colKey: null });

const startEdit = (rowIndex, colKey) => {
    console.log(props.metaData.columnData)
    const colData = props.metaData.columnData.find(d=>d.key == colKey)

    if(!!colData && colData.isEditable)
        editingCell.value = { rowIndex, colKey };
};

const stopEdit = () => {
  editingCell.value = { rowIndex: null, colKey: null };
};

const flatRows = computed(() => {
  if (!props.reportData?.skuModels || !props.metaData?.rowDefinitions) {
    return [];
  }
  const rows = flattenRows(props.reportData.skuModels, props.metaData.rowDefinitions,props.metaData.columnData)
  const total = getTotal(props.reportData, props.metaData.rowDefinitions,props.metaData.columnData);
  return [...rows,...total]
});
const format = (val, colKey) => {
  if (val === null) return '—';
  if (colKey === 'contributionalGrowth') return (val * 100).toFixed(2) + '%';
  return typeof val === 'number' ? val.toLocaleString() : val;
};
const vFocus = {
  mounted: (el) => el.focus()
};
console.log(flatRows)

</script>

<template>
  <div class="bg-[#121212] p-4 rounded-lg overflow-x-auto">
    <table class="w-full border-collapse text-[11px]">
      <thead>
        <tr class="border-b border-gray-700 text-gray-500 uppercase">
          <th v-for="col in metaData.columnData" :key="col.key" class="p-3 text-left">
            {{ col.title }}
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, idx) in flatRows" :key="idx" class="border-b border-gray-800 hover:bg-gray-800/40">
          <td v-for="col in metaData.columnData" :key="col.key" class="p-2">
            <div v-if="editingCell.rowIndex === idx && editingCell.colKey === col.key">
              <input 
                v-model="row[col.key]"
                v-focus
                @blur="stopEdit"
                @keyup.enter="stopEdit"
                class="bg-[#1e1e1e] text-blue-400 border border-blue-500 rounded px-2 py-0.5 w-full outline-none"            </div>
            <div 
              v-else 
              @dblclick="startEdit(idx, col.key)"
              class="cursor-pointer min-h-[1.5rem] flex items-center"
              :class="{'font-bold text-blue-400': col.key === 'type', 'text-blue': col.key === 'sku'}"
            >
              {{ format(row[col.key], col.key) }}
            </div>

          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
/* Основной контейнер таблицы */
.report-wrapper {
  background-color: #121212; /* Глубокий черный фон как на скрине */
  color: #d1d5db;
  font-family: 'Inter', -apple-system, sans-serif;
  padding: 2rem;
  min-height: 100vh;
}

table {
  width: 100%;
  border-collapse: collapse;
  background-color: transparent;
}

/* Заголовки колонок */
thead th {
  color: #9ca3af;
  font-size: 0.75rem;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 0.05em;
  padding: 1rem;
  border-bottom: 2px solid #262626;
  text-align: left;
}

/* Ячейки таблицы */
tbody td {
  padding: 0.75rem 1rem;
  border-bottom: 1px solid #1f1f1f; /* Тонкие разделители строк */
  font-size: 0.85rem;
  vertical-align: middle;
}

/* Жирный белый текст для названий SKU и значений */
.text-white {
  color: #ffffff;
  font-weight: 600;
}

.tabular-nums {
  font-variant-numeric: tabular-nums;
  text-align: right;
}

.type-column {
  color: #9ca3af;
  text-transform: uppercase;
  font-size: 0.7rem;
  font-weight: 700;
  letter-spacing: 0.1em;
}

tbody tr:hover {
  background-color: rgba(255, 255, 255, 0.03);
}

</style>




