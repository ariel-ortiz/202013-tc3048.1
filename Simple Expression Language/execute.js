#!/usr/bin/env node

const fileName = './output.wat';
const fs = require('fs');

const importArgs = {
  math: {
    pow: (x, y) => Math.pow(x, y)
  }
};

async function getInstance() {
  try {
    const fileContent = fs.readFileSync(fileName, 'utf8');
    const wabt = await require('wabt')();
    const wasmModule = wabt.parseWat(fileName, fileContent);
    const buffer = wasmModule.toBinary({}).buffer;
    const module = await WebAssembly.compile(buffer);
    return WebAssembly.instantiate(module, importArgs);
  } catch (error) {
    console.log(error.message);
    process.exit(1);
  }
}

async function main() {
  const instance = await getInstance();

  const start = instance.exports.start;
  console.log(start());
}

main();