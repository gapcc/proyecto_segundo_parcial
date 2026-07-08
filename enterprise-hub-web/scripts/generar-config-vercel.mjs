import { mkdir, writeFile } from 'node:fs/promises';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const publicDir = join(__dirname, '..', 'public');
const configPath = join(publicDir, 'config.js');
const apiBaseUrl = process.env.API_BASE_URL || 'http://localhost:5156/api';

await mkdir(publicDir, { recursive: true });
await writeFile(
  configPath,
  `globalThis.ENTERPRISE_HUB_CONFIG = ${JSON.stringify({ apiBaseUrl }, null, 2)};\n`
);