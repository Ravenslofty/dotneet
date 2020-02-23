type CPURegisters = {
  reg: uint16 array;
}

type Register = {
  idx: int
}

let register_of_int idx =
  if idx >= 0 && idx <= 7 then
    Some({ idx = idx })
  else
    None

let int_of_register reg = reg.idx

