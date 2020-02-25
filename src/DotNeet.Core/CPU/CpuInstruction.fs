module CpuInstruction

type SegmentOverride = NoOverride | OverrideEnabled
type RepeatPrefix = NoRepeat| RepeatZero | RepeatNonZero

// Not sure of best implementation here yet,
// it'll end up being a series of lookup tables
type OpCodeName = Add
type OpCode =
    { Code: byte; Name: OpCodeName }

let opCodes = [|
    { Code = 0x00uy; Name = Add; }
|]
