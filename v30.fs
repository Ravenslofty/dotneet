(* NEC V30 CPU implementation *)

(* Wrapper type around a register array that only accepts Registers *)
type CPURegisters = {
  reg: uint16 array;
}

(* Wrapper type around a V30 register to avoid out-of-bound access *)
type Register = {
  idx: int
}

(* Create a Register from an integer *)
let register_of_int idx =
  if idx >= 0 && idx <= 7 then
    Some({ idx = idx })
  else
    None

(* Return the encapsulated integer of a Register *)
let int_of_register reg = reg.idx

