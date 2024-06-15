using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
// ReSharper disable NotAccessedField.Local

namespace MethodCalls
{
  // [MemoryDiagnoser]
  [SimpleJob(RuntimeMoniker.Net472)]
  [SimpleJob(RuntimeMoniker.Net80)]
  [DisassemblyDiagnoser]
  public class Benchmark
  {
    private const int iterations = 1024;
    private int myA = 1;
    
    private int mySideEffect0 = 0;
    private int mySideEffect1 = 0;
    private int mySideEffect2 = 0;
    private int mySideEffect3 = 0;

    public event Action<int> EventSingle0;
    public event Action<int> EventSingle1;
    public event Action<int> EventSingle2;
    public event Action<int> EventSingle3;
    
    public event Action<int> EventDouble0;
    public event Action<int> EventDouble1;

    public Benchmark()
    {
      EventSingle0 += (a) => { mySideEffect0 = a; };
      EventSingle1 += (a) => { mySideEffect1 = a; };
      EventSingle2 += (a) => { mySideEffect2 = a; };
      EventSingle3 += (a) => { mySideEffect3 = a; };
      
      EventDouble0 += (a) => { mySideEffect0 = a; };
      EventDouble0 += (a) => { mySideEffect0 = a; };
      EventDouble1 += (a) => { mySideEffect1 = a; };
      EventDouble1 += (a) => { mySideEffect1 = a; };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlinedCall0(int a) => mySideEffect0 = a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlinedCall1(int a) => mySideEffect1 = a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlinedCall2(int a) => mySideEffect2 = a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlinedCall3(int a) => mySideEffect3 = a;


    [MethodImpl(MethodImplOptions.NoInlining)]
    public void NotInlinedCall0(int a) => mySideEffect0 = a;
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void NotInlinedCall1(int a) => mySideEffect1 = a;
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void NotInlinedCall2(int a) => mySideEffect2 = a;
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void NotInlinedCall3(int a) => mySideEffect3 = a;

    [Benchmark(Baseline = true)]
    public void InlinedCall()
    {
      for (int i = 0; i < iterations; i += 4)
      {
        InlinedCall0(myA);
        InlinedCall1(myA);
        InlinedCall2(myA);
        InlinedCall3(myA);
      }  
    }
    
    [Benchmark]
    public void NotInlinedCall()
    {
      for (int i = 0; i < iterations; i += 4)
      {
        NotInlinedCall0(myA);
        NotInlinedCall1(myA);
        NotInlinedCall2(myA);
        NotInlinedCall3(myA);
      }  
    }
    
    [Benchmark]
    public void EventInvoke()
    {
      for (int i = 0; i < iterations; i += 4)
      {
        EventSingle0.Invoke(myA);
        EventSingle1.Invoke(myA);
        EventSingle2.Invoke(myA);
        EventSingle3.Invoke(myA);
      }  
    }
    
    [Benchmark]
    public void EventInvokeTwoListeners()
    {
      for (int i = 0; i < iterations; i += 4)
      {
        EventDouble0.Invoke(myA);
        EventDouble1.Invoke(myA);
      }  
    }
  }
  
  internal class Program
  {
    public static void Main(string[] args)
    {
      BenchmarkRunner.Run<Benchmark>();
    }
  }
}