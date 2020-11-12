;; Simple WebAssembly example.

(module
  (import "math" "sin" (func $sin (param f32) (result f32)))
  (import "math" "cos" (func $cos (param f32) (result f32)))

  (func
    ;; Function signature: i32 duplicate(i32 $x)
    (export "duplicate")
    (param $x i32)
    (result i32)

    ;; Body of the function
    local.get $x
    i32.const 2
    i32.mul
  )

  (func
    (export "fahrenheit2celsius")
    (param $F f32)
    (result f32)

    ;; 5/9
    f32.const 5
    f32.const 9
    f32.div

    ;; F - 32
    local.get $F
    f32.const 32
    f32.sub

    f32.mul
  )

  ;; $plus1(3 * $x + $sin($y))
  (func
    (export "pruebita")
    (param $x f32)
    (param $y f32)
    (result f32)

    ;; 3 * $x
    f32.const 3
    local.get $x
    f32.mul

    ;; $sin($y)
    local.get $y
    call $sin

    f32.add
    call $plus1
  )

  ;; Definir función local a este módulo
  (func $plus1
    (param $x f32)
    (result f32)
    f32.const 1
    local.get $x
    f32.add
  )
)
