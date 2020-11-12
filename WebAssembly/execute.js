#!/usr/bin/env node

const fileName = './example.wat';
const fs = require('fs');

require('wabt')().then(wabt => {
  const fileContent = fs.readFileSync(fileName, 'utf8');
  const wasmModule = wabt.parseWat(fileName, fileContent);
  const buffer = wasmModule.toBinary({}).buffer;
  return WebAssembly.compile(buffer);
})
.then(module => {
  return WebAssembly.instantiate(module, {});
})
.then(instance => {
  const duplicate = instance.exports.duplicate;
  console.log(duplicate(5));
})
.catch(error => {
  console.log(error.message);
  process.exit(1);
});