const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7298';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/analysis",
      "/api/analysis/GetDTColumns",
      "/api/analysis/ExploratoryColumns",
      "/api/upload"
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
