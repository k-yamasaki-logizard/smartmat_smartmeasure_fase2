<script setup lang="ts">
import { ref } from 'vue'

defineProps<{ msg: string }>()

const count = ref(0)
</script>

<template>
  <h1>{{ msg }}</h1>

  <div class="p-8">
    <button
      type="button"
      class="rounded-lg border border-transparent py-[0.6em] px-[1.2em] text-base font-medium font-inherit bg-neutral-900 cursor-pointer transition-[border-color_0.25s] hover:border-[#646cff] focus:outline focus:outline-4 focus:outline-[webkit-focus-ring-color]"
      @click="count++"
    >
      count is {{ count }}
    </button>
    <p>
      Edit
      <code class="font-mono">components/HelloWorld.vue</code> to test HMR
    </p>
  </div>

  <p>
    Check out
    <a href="https://vuejs.org/guide/quick-start.html#local" target="_blank" class="font-medium text-[#646cff] no-underline hover:text-[#535bf2]"
      >create-vue</a
    >, the official Vue + Vite starter
  </p>
  <p>
    Learn more about IDE Support for Vue in the
    <a
      href="https://vuejs.org/guide/scaling-up/tooling.html#ide-support"
      target="_blank"
      class="font-medium text-[#646cff] no-underline hover:text-[#535bf2]"
      >Vue Docs Scaling up Guide</a
    >.
  </p>
  <p class="text-gray-500">Click on the Vite and Vue logos to learn more</p>
</template>
