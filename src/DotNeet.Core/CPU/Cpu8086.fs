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
    | SI -> 0us
    | DI -> 0us
    | SP -> 0us
    | BP -> 0us
    | CS -> 0us
    | DS -> 0us
    | SS -> 0us
    | ES -> 0us
    | IP -> 0us
    | Flags -> 0us

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
    | SI -> ()
    | DI -> ()
    | SP -> ()
    | BP -> ()
    | CS -> ()
    | DS -> ()
    | SS -> ()
    | ES -> ()
    | IP -> ()
    | Flags -> ()


