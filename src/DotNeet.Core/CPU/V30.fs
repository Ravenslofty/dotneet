(* NEC V30 CPU implementation *)
module V30

(* Wrapper type around a V30 general-purpose register to avoid out-of-bounds access *)
type Register =
    { reg: int }
    
(* Create a Register from an integer *)
let registerByIndex idx =
    if idx >= 0 && idx <= 7 then Some({ reg = idx }) else None

(* Return the encapsulated integer of a Register *)
let registerValue reg = reg.reg

(* Register constants for ease of debugging and testing *)
(* Accumulator Register *)
let AxRegister = { reg = 0 }
(* Count Register *)
let CxRegister = { reg = 1 }
(* Data Register *)
let DxRegister = { reg = 2 }
(* Base Register *)
let BxRegister = { reg = 3 }
(* Source Index Register *)
let SiRegister = { reg = 4 }
(* Destination Index Register *)
let DiRegister = { reg = 5 }
(* Base Pointer Register *)
let BpRegister = { reg = 6 }
(* Stack Pointer Register *)
let SpRegister = { reg = 7 }


(* Wrapper type around a V30 segment register to avoid out-of-bounds access *)
type Segment =
    { seg: int }

(* Create a Segment from an integer *)
let segmentByIndex idx =
    if idx >= 0 && idx <= 3 then Some({ seg = idx }) else None

(* Return the encapsulated integer of a Segment *)
let segmentValue seg = seg.seg

(* Segment constants for ease of debugging and testing *)
(* Code Segment *)
let CsSegment = { seg = 0 }
(* Data Segment *)
let DsSegment = { seg = 1 }
(* Extra Segment *)
let EsSegment = { seg = 2 }
(* Stack Segment *)
let SsSegment = { seg = 3 }


(* CPU status word flags *)
[<System.FlagsAttribute>]
type Flags =
    (* The last operation resulted in unsigned overflow *)
    | Carry = 1us
    (* The least significant byte of the last operation has an even number of bits *)
    | Parity = 4us
    (* The last operation produced a carry from bit 3 to bit 4 *)
    | Adjust = 16us
    (* The last operation produced a zero result *)
    | Zero = 64us
    (* The last operation produced a negative result *)
    | Sign = 128us
    (* The CPU is in single-step mode and will produce an interrupt after executing the next instruction *)
    | Trap = 256us
    (* The CPU will respond to external interrupts *)
    | Interrupt = 512us
    (* The CPU will execute string operations from high addresses to low addresses instead of low to high *)
    | Direction = 1024us
    (* The last operation resulted in signed overflow *)
    | Overflow = 2048us


(* Wrapper type around a register array that only accepts Registers *)
type CPURegisters =
    { (* The general-purpose register file *)
      reg: uint16 array
      (* The current instruction pointer *)
      ip: uint16
      (* The current memory segments *)
      seg: uint16 array
      (* The CPU status word *)
      flags: Flags }

(* Return a general-purpose register *)
let getRegister cpu gpr = cpu.reg.[registerValue gpr]

(* Return the CPU instruction pointer *)
let getInstructionPointer cpu = cpu.ip

(* Return a segment register *)
let getSegment cpu seg = cpu.seg.[segmentValue seg]

(* Return the CPU status word *)
let getFlags cpu = cpu.flags
