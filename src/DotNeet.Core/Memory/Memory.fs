(* 384kb 32-bit RAM *)
module Memory

let busWidth = 16
let ramSize = 384 * 1024 * busWidth / 8

let data = Array.create ramSize 0uy
let zero = System.Array.Fill(data, 0uy)

let readByte addr = uint32 data.[addr]
let readShort addr = readByte (addr + 1) <<< 8 ||| readByte addr
let readInt addr = readShort (addr + 2) <<< 16 ||| readShort addr

let writeByte addr (value: byte) = data.[addr] <- value
let writeBytes addr (values: byte[]) = System.Array.Copy(values, 0, data, addr, values.Length)
