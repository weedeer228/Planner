
<script setup>
import { LoadReport } from './shared/report-service';
import Report from './components/report-component.vue'

const { reportData, metaData, isLoading, error } = LoadReport()

</script>

<template>
  
  <div class="report-container">
    <div v-if="isLoading" class="loader">Загрузка данных...</div>

    <div v-else-if="error" class="error-msg">{{ error }}</div>
    <Report 
      v-if="metaData" 
      :reportData="reportData" 
      :metaData="metaData" 
    />
  </div>
</template>

<style>
body {
  background-color: #121212; 
  color: #d1d5db;
  margin: 0;
  min-height: 100vh;
}
</style>
