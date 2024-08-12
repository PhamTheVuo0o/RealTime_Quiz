import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port:4200,
    historyApiFallback: true
  },
  plugins: [
    react(),
    tsconfigPaths()
  ],
  define: {
    'process.env': process.env
  }
})
