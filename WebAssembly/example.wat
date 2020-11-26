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

  (func
    ;; bool positive_even(int x)
    (export "positive_even")
    (param $x i32)
    (result i32)

    ;; $x >= 0 && $x % 2 == 0

    ;; $x >= 0
    local.get $x
    i32.const 0
    i32.ge_s

    if (result i32)
      ;; $x % 2 == 0
      local.get $x
      i32.const 2
      i32.rem_s
      i32.const 0
      i32.eq
    else
      i32.const 0
    end
  )

  (func
    ;; bool positive_even(int x)
    (export "fact")
    (param $x i32)
    (result i32)
    (local $r i32)
    (local $i i32)

    ;; r = 1
    i32.const 1
    local.set $r

    ;; i = 1
    i32.const 1
    local.set $i

    block $label1
      loop $label2
        ;; $i <= $x
        local.get $i
        local.get $x
        i32.le_s

        i32.eqz ;; not
        br_if $label1  ;; break

        ;; r *= i
        local.get $r
        local.get $i
        i32.mul
        local.set $r

        ;; i += 1
        local.get $i
        i32.const 1
        i32.add
        local.set $i

        br $label2 ;; continue
      end  ;; loop
    end ;; block
    local.get $r
  )
)
