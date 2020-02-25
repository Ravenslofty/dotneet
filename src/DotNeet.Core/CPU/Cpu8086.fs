module Cpu8086

type OperatingMode = Real | Protected

type Register =
    | AX | BX | CX | DX    // General purpose registers

type Index =
    | SI | DI | SP | BP    // Index Registers (Source, Destination, Stack, Base)
    
type Segment =
    | CS | DS | SS | ES    // Segment Registers (Code, Data, Stack, Extra)

type SpecialRegister = 
    | IP | Flags           // Program Counter and Status Register

type RegisterSize =
    | High | Low | Word    // The part of the register to manipulate during read/write

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
    | CS -> 8
    | DS -> 9
    | SS -> 10
    | ES -> 11

let ipIndex = 12
let flagsIndex = 13

let readRegister reg size =
    let reg = registerData.[registerIndex reg]

    let get = match size with
    | Low -> (fun x -> lowByte x |> uint16)
    | High -> (fun x -> highByte x |> uint16)
    | Word -> (fun x -> x)

    get reg

let writeRegister reg size (value: uint16) =
    let oldValue = registerData.[registerIndex reg]

    let newValue = match size with
    | Low ->  (value &&& 0x00FFus) ||| (oldValue &&& 0xFF00us)
    | High -> (value <<< 8) ||| (oldValue &&& 0x00FFus)
    | Word -> value

    registerData.[registerIndex reg] <- newValue

let readSegment seg = 
    match seg with
    | CS -> registerData.[segmentIndex CS]
    | DS -> registerData.[segmentIndex DS]
    | SS -> registerData.[segmentIndex SS]
    | ES -> registerData.[segmentIndex ES]

let writeSegment seg (value: uint16) =
    match seg with
    | CS -> registerData.[segmentIndex CS] <- value
    | DS -> registerData.[segmentIndex DS] <- value
    | SS -> registerData.[segmentIndex SS] <- value
    | ES -> registerData.[segmentIndex ES] <- value

let readIp () = registerData.[ipIndex]
let writeIp value = registerData.[ipIndex] <- value

let readFlag (flag: CpuFlag) = registerData.[flagsIndex] &&& uint16 flag
let writeFlag (flag: CpuFlag) = registerData.[flagsIndex] <- registerData.[flagsIndex] ||| uint16 flag

let readFlags () = registerData.[flagsIndex]
let writeFlags value = registerData.[flagsIndex] <- value

let flagIsSet (flag: CpuFlag) = readFlag flag > 0us

// Check to see if we need to cache the left-shift operation, since we switch segments less than we access memory
let readMemory offset = Memory.readShort (int (readSegment ES) <<< 4 + offset)
let writeMemory offset value = Memory.writeShort (int (readSegment ES) <<< 4 + offset) value

let resetCpu () =
    System.Array.Clear(registerData, 0, registerData.Length)
    writeSegment CS 0xFFFFus
    writeFlags 0xF002us
    
let runCpu () =
    let mutable running = true
    let segmentOverride = ()
    let repeatPrefix = ()
    // Design halting mechanism
    while running do
        // Get OpCode, parameters
        // Execute on that opcode
        ()
    ()
