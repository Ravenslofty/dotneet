<<<<<<< HEAD:v30.fs
(* NEC V30 CPU implementation *)

(* Wrapper type around a V30 general-purpose register to avoid out-of-bounds access *)
type Register =
    { reg: int }

(* Create a Register from an integer *)
let registerOfInt idx =
    if idx >= 0 && idx <= 7 then Some({ reg = idx }) else None
=======
﻿module V30
(* NEC V30 CPU implementation *)

(* Wrapper type around a V30 general-purpose register to avoid out-of-bounds access *)
type Register = {
    reg: int
}

(* Create a Register from an integer *)
let registerOfInt idx =
    if idx >= 0 && idx <= 7 then
        Some({ reg = idx })
    else
        None
>>>>>>> Create solution structure and unit test project:src/DotNeet.Core/CPU/V30.fs

(* Return the encapsulated integer of a Register *)
let intOfRegister reg = reg.reg

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
<<<<<<< HEAD:v30.fs
type Segment =
    { seg: int }

(* Create a Segment from an integer *)
let segmentOfInt idx =
    if idx >= 0 && idx <= 3 then Some({ seg = idx }) else None
=======
type Segment = {
    seg: int
}

(* Create a Segment from an integer *)
let segmentOfInt idx =
    if idx >= 0 && idx <= 3 then
        Some({seg = idx})
    else
        None
>>>>>>> Create solution structure and unit test project:src/DotNeet.Core/CPU/V30.fs

(* Return the encapsulated integer of a Segment *)
let intOfSegment seg = seg.seg

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
<<<<<<< HEAD:v30.fs
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
=======
    | Carry     = 0x0001us
    (* The least significant byte of the last operation has an even number of bits *)
    | Parity    = 0x0004us
    (* The last operation produced a carry from bit 3 to bit 4 *)
    | Adjust    = 0x0010us
    (* The last operation produced a zero result *)
    | Zero      = 0x0040us
    (* The last operation produced a negative result *)
    | Sign      = 0x0080us
    (* The CPU is in single-step mode and will produce an interrupt after executing the next instruction *)
    | Trap      = 0x0100us
    (* The CPU will respond to external interrupts *)
    | Interrupt = 0x0200us
    (* The CPU will execute string operations from high addresses to low addresses instead of low to high *)
    | Direction = 0x0400us
    (* The last operation resulted in signed overflow *)
    | Overflow  = 0x0800us


(* Wrapper type around a register array that only accepts Registers *)
type CPURegisters = {
    (* The general-purpose register file *)
    reg: uint16 array;
    (* The current instruction pointer *)
    ip: uint16;
    (* The current memory segments *)
    seg: uint16 array;
    (* The CPU status word *)
    flags: Flags;
}
>>>>>>> Create solution structure and unit test project:src/DotNeet.Core/CPU/V30.fs

(* Return a general-purpose register *)
let getRegister cpu gpr = cpu.reg.[intOfRegister gpr]

(* Return the CPU instruction pointer *)
let getInstructionPointer cpu = cpu.ip

(* Return a segment register *)
let getSegment cpu seg = cpu.seg.[intOfSegment seg]

(* Return the CPU status word *)
<<<<<<< HEAD:v30.fs
let getFlags cpu = cpu.flags
=======
let getFlags cpu = cpu.flags
>>>>>>> Create solution structure and unit test project:src/DotNeet.Core/CPU/V30.fs
