(* 384kb 32-bit RAM *)
module Memory

let busWidth = 16
let ramSize = 384 * 1024 * busWidth / 8
let wordSize = busWidth / 8

let data = Array.create ramSize 0uy
let zero = System.Array.Fill(data, 0uy)

let readShift addr bits = uint32 data.[addr] <<< bits

let readByte addr = data.[addr]
let readShort addr = readShift (addr + 1) 8 ||| uint32 (readByte addr)
let readInt addr = readShort (addr + 2) <<< 16 ||| readShort addr

let writeByte addr (value: byte) = data.[addr] <- value
let writeBytes addr (values: byte[]) = System.Array.Copy(values, 0, data, addr, values.Length)
