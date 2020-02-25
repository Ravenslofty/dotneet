module Cpu8086

type OperatingMode = Real | Protected

type Register =
    | AX                    // General purpose registers
    | BX
    | CX
    | DX

type Index =
    | SI                   // Index Registers (Source, Destination, Stack, Base)
    | DI
    | SP
    | BP

type Segment =
    | CS | DS | SS | ES    // Segment Registers (Code, Data, Stack, Extra)

type SpecialRegister = 
    | IP | Flags           // Program Counter and Status Register

type RegisterSize =
    | High
    | Low
    | Word

type CpuFlag =
    | Carry     = 0x0001us // The last operation resulted in unsigned overflow
    | Parity    = 0x0004us // The least significant byte of the last operation has an even number of bits
    | Adjust    = 0x0010us // The last operation produced a carry from bit 3 to bit 4
    | Zero      = 0x0040us // The last operation produced a zero result
    | Sign      = 0x0080us // The last operation produced a negative result
    | Trap      = 0x0100us // The CPU is in single-step mode and will produce an interrupt after executing the next instruction
    | Interrupt = 0x0200us // The CPU will respond to external interrupts
    | Direction = 0x0400us // The CPU will execute string operations from high addresses to low addresses instead of low to high
    | Overflow  = 0x0800us // The last operation resulted in signed overflow

let lowByte (s: uint16) = byte s
let highByte (s: uint16) = byte (s >>> 8)

let registerData = Array.create 14 0us

let registerIndex = function
    | AX -> 0
    | BX -> 1
    | CX -> 2
    | DX -> 3

let segmentIndex = function
    | 

let readRegister reg size =
    let reg = registerData.[registerIndex reg]

    let get = match size with
    | Low -> (fun x -> lowByte x |> uint16)
    | High -> (fun x -> highByte x |> uint16)
    | Word -> (fun x -> x)

    get reg

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

let readIp = registerData.[12]
let writeIp value = registerData.[12] <- value

let readFlag (flag: CpuFlag) = registerData.[13] &&& uint16 flag
let writeFlag (flag: CpuFlag) = registerData.[13] <- registerData.[13] ||| uint16 flag

let readFlags = registerData.[13]
let writeFlags value = registerData.[13] <- value

let flagIsSet (flag: CpuFlag) = readFlag flag > 0us

// Check to see if we need to cache the left-shift operation, since we switch segments less than we access memory
let readMemory offset = Memory.readShort (int (readSegment ES) <<< 4 + offset)
let writeMemory offset value = Memory.writeShort (int (readSegment ES) <<< 4 + offset) value

let resetCpu =
    System.Array.Clear(registerData, 0, 0)
    writeSegment CS 0xFFFFus
    writeFlags 0xF002us
    
let runCpu =
    let mutable running = false
    let segmentOverride = ()
    let repeatPrefix = ()
    // Design halting mechanism
    while running do
        // Get OpCode, parameters
        // Execute on that opcode
        ()
    ()
