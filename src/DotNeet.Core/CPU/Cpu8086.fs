module Cpu8086

type OperatingMode = Real | Protected

type Register =
    | AH | AL | AX        // General purpose Registers
    | BH | BL | BX
    | CH | CL | CX
    | DH | DL | DX
    | SI | DI | SP | BP   // Index Registers (Source, Destination, Stack, Base)
    | CS | DS | SS | ES   // Segment Registers (Code, Data, Stack, Extra)
    | IP | Flags          // Program Counter and Status Register

let lowByte (s: uint16) = byte s
let highByte (s: uint16) = byte (s >>> 8)

let registerData = Array.create 14 0us

let readRegister reg =
    match reg with
    | AX -> registerData.[0]
    | AH -> highByte registerData.[0] |> uint16
    | AL -> lowByte registerData.[0] |> uint16
    | BX -> registerData.[1]
    | BH -> highByte registerData.[1] |> uint16
    | BL -> lowByte registerData.[1] |> uint16
    | CX -> registerData.[2]
    | CH -> highByte registerData.[2] |> uint16
    | CL -> lowByte registerData.[2] |> uint16
    | DX -> registerData.[3]
    | DH -> highByte registerData.[3] |> uint16
    | DL -> lowByte registerData.[3] |> uint16
    | _ -> failwith "Invalid register specified"

let writeRegister reg (value: uint16) =
    match reg with
    | AX -> registerData.[0] <- value
    | AH -> registerData.[0] <- (value <<< 8) ||| (registerData.[0] &&& 0x00FFus)
    | AL -> registerData.[0] <- (value &&& 0x00FFus) ||| (registerData.[0] &&& 0xFF00us)
    | BX -> registerData.[1] <- value
    | BH -> registerData.[1] <- (value <<< 8) ||| (registerData.[1] &&& 0x00FFus)
    | BL -> registerData.[1] <- (value &&& 0x00FFus) ||| (registerData.[1] &&& 0xFF00us)
    | CX -> registerData.[2] <- value
    | CH -> registerData.[2] <- (value <<< 8) ||| (registerData.[2] &&& 0x00FFus)
    | CL -> registerData.[2] <- (value &&& 0x00FFus) ||| (registerData.[2] &&& 0xFF00us)
    | DX -> registerData.[3] <- value
    | DH -> registerData.[3] <- (value <<< 8) ||| (registerData.[3] &&& 0x00FFus)
    | DL -> registerData.[3] <- (value &&& 0x00FFus) ||| (registerData.[3] &&& 0xFF00us)
    | _ -> failwith "Invalid register specified"

let readSegment seg = 
    match seg with
    | CS -> registerData.[8]
    | DS -> registerData.[9]
    | SS -> registerData.[10]
    | ES -> registerData.[11]
    | _ -> failwith "Invalid segment specified"

let writeSegment seg (value: uint16) =
    match seg with
    | CS -> registerData.[8] <- value
    | DS -> registerData.[9] <- value
    | SS -> registerData.[10] <- value
    | ES -> registerData.[11] <- value
    | _ -> failwith "Invalid segment specified"

// Check to see if we need to cache the left-shift operation, since we switch segments less than we access memory
let readMemory offset = Memory.readShort (int (readSegment ES) <<< 4 + offset)
let writeMemory offset value = Memory.writeShort (int (readSegment ES) <<< 4 + offset) value
