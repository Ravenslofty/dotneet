namespace DotNeet.Core.Tests.Cpu

open NUnit.Framework
open Cpu8086

[<TestFixture>]
type Cpu8086Tests () =

    [<SetUp>]
    member this.SetUp() =
        resetCpu

    [<Test>]
    member this.CpuInitialStateCheck() =
        let registers = [AX; BX; CX; DX;]
        let clearedSegments = [DS; SS; ES;]

        for reg in registers do
            Assert.That(readRegister reg, Is.EqualTo(0), fun _ -> "");

        for seg in clearedSegments do
            Assert.That(readSegment seg, Is.EqualTo(0));

        let csSegment = readSegment CS
        let flags = readFlags
        
        Assert.That(csSegment, Is.EqualTo(0xFFFFus))
        Assert.That(flags, Is.EqualTo(0xF002us))

    [<Test>]
    member this.CanReadAndWriteFullRegisters() =
        let registers = [AX; BX; CX; DX;]
        //let upperHalfRegisters = [AH; BH; CH; DH;]
        //let lowerHalfRegisters = [AL; BL; CL; DL;]

        let testValue = 0xAAFFus
        let testValueLow = 0xFFus
        let testValueHigh = 0xAAus

        for reg in registers do
            writeRegister reg testValue
            Assert.That(readRegister reg, Is.EqualTo(testValue));
        
        //for reg in upperHalfRegisters do
        //    Assert.That(readRegister reg, Is.EqualTo(testValueHigh));

        //for reg in lowerHalfRegisters do
        //    Assert.That(readRegister reg, Is.EqualTo(testValueLow));

    [<Test>]
    member this.CanReadAndWriteSegments() =
        let segments = [CS; DS; SS; ES;]
        let testValue = 0xAAFFus

        for seg in segments do
            writeSegment seg testValue
            Assert.That(readSegment seg, Is.EqualTo(testValue));
