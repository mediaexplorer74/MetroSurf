// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Adapters.PropositionViewModelsComparer
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System.Collections.Generic;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel.Adapters
{
  public class PropositionViewModelsComparer : IComparer<PropositionViewModel>
  {
    int IComparer<PropositionViewModel>.Compare(PropositionViewModel x, PropositionViewModel y)
    {
      return x.DaysCount >= y.DaysCount ? 1 : -1;
    }
  }
}
