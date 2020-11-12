#!/usr/bin/env node

const fileName = './example.wat';
const fs = require('fs');

const importArgs = {
  math: {
    sin: x => Math.sin(x),
    cos: x => Math.cos(x)
  }
};

async function getInstance() {
  const fileContent = fs.readFileSync(fileName, 'utf8');
  const wabt = await require('wabt')();
  const wasmModule = wabt.parseWat(fileName, fileContent);
  const buffer = wasmModule.toBinary({}).buffer;
  const module = await WebAssembly.compile(buffer);
  return WebAssembly.instantiate(module, importArgs);
}

async function main() {
  try {
    const instance = await getInstance();
    const { duplicate, fahrenheit2celsius, pruebita } = instance.exports;
    console.log(duplicate(15));
    console.log(fahrenheit2celsius(212));
    console.log(fahrenheit2celsius(32));
    console.log(fahrenheit2celsius(-40));
    console.log(pruebita(10.1, 5.2));
  } catch (err) {
    console.error(err);
  }
}

main();

