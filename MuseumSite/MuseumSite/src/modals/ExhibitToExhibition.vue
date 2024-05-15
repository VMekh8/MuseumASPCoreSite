<template>
  <b-modal v-model="visible" title="Взаємодія експонатів з виставкою" hide-footer no-close-on-backdrop>
    <form @submit.prevent="onOk">
      <div class="mb-3">
        <label for="number1" class="form-label">Ідентифікатор виставки:</label>
        <input type="number" id="number1" v-model="number1" class="form-control" />
      </div>
      <div class="mb-3">
        <label for="number2" class="form-label">Ідентифікатор експонату:</label>
        <input type="number" id="number2" v-model="number2" class="form-control" />
      </div>
      <div class="d-flex justify-content-end">
        <b-button variant="secondary" @click="onCancel">Скасувати</b-button>
        <b-button variant="primary" type="submit">OK</b-button>
      </div>
    </form>
  </b-modal>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue';

export default defineComponent({
  name: 'ModalComponent',
  props: {
    modelValue: {
      type: Boolean,
      required: true
    }
  },
  emits: ['update:modelValue', 'ok', 'cancel'],
  setup(props, { emit }) {
    const visible = ref(props.modelValue);

    watch(() => props.modelValue, (newVal) => {
      visible.value = newVal;
    });

    watch(visible, (newVal) => {
      emit('update:modelValue', newVal);
    });

    const number1 = ref<number | null>(null);
    const number2 = ref<number | null>(null);

    const onOk = () => {
      emit('ok', { number1: number1.value, number2: number2.value });
      visible.value = false;
    };

    const onCancel = () => {
      emit('cancel');
      visible.value = false;
    };

    return {
      visible,
      number1,
      number2,
      onOk,
      onCancel
    };
  }
});
</script>

<style scoped>
.modal-content {
  padding: 20px;
}
</style>
