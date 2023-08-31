import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import ViteDotNet from "vite-dotnet";
import basicSsl from "@vitejs/plugin-basic-ssl";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), ViteDotNet("src/main.jsx"), basicSsl()],
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7284",
        changeOrigin: true,
        secure: false,
        ws: true,
      },
    },
  },
});
