// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Adapters.BaseAdapter`2
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System.Threading.Tasks;

#nullable disable
namespace VPN.ViewModel.Adapters
{
  public abstract class BaseAdapter<TInput, TOutput>
    where TInput : class
    where TOutput : class, new()
  {
    public TOutput Convert(TInput input)
    {
      TOutput output = new TOutput();
      this.Init(input, output);
      return output;
    }

    public abstract void Init(TInput input, TOutput output);

    public virtual async Task InitAsync(TInput input, TOutput output)
    {
    }
  }
}
