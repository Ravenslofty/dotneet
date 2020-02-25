(* 384kb 32-bit RAM *)
module Memory

let ramSize = 384 * 1024

let data = Array.create ramSize 0us
let fill = Array.fill data
let zero = fill 0 data.Length 0us

let readByte addr = uint32 (byte data.[addr])
let readShort addr = readByte (addr + 1) <<< 8 ||| readByte addr
let readInt addr = readShort (addr + 2) <<< 16 ||| readShort addr

let writeByte addr (value: byte) = data.[addr] <- uint16 value
let writeShort addr (value: uint16) = data.[addr] <- value &&& 0xFFus; data.[addr + 1] <- value >>> 8
let writeInt addr (value: uint32) = writeShort addr (uint16 (value &&& 0xFFFFu)); writeShort (addr + 2) (uint16 (value >>> 16))

