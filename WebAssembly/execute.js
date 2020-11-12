#!/usr/bin/env node

const fileName = './example.wat';
const fs = require('fs');

const importArgs = {
  math: {
    sin: x => Math.sin(x),
    cos: x => Math.cos(x)
  }
};

require('wabt')().then(wabt => {
  const fileContent = fs.readFileSync(fileName, 'utf8');
  const wasmModule = wabt.parseWat(fileName, fileContent);
  const buffer = wasmModule.toBinary({}).buffer;
  return WebAssembly.compile(buffer);
})
.then(module => {
  return WebAssembly.instantiate(module, importArgs);
})
.then(instance => {
  const { duplicate, fahrenheit2celsius, pruebita } = instance.exports;
  console.log(duplicate(15));
  console.log(fahrenheit2celsius(212));
  console.log(fahrenheit2celsius(32));
  console.log(fahrenheit2celsius(-40));
  console.log(pruebita(10.1, 5.2));
})
.catch(error => {
  console.log(error.message);
  process.exit(1);
});