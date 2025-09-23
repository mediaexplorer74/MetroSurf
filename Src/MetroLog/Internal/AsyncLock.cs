// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.AsyncLock
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Internal
{
  internal class AsyncLock
  {
    private readonly SemaphoreSlim m_semaphore;
    private readonly Task<AsyncLock.Releaser> m_releaser;

    public AsyncLock()
    {
      this.m_semaphore = new SemaphoreSlim(1);
      this.m_releaser = Task.FromResult<AsyncLock.Releaser>(new AsyncLock.Releaser(this));
    }

    public Task<AsyncLock.Releaser> LockAsync()
    {
      Task task = this.m_semaphore.WaitAsync();
      return !task.IsCompleted ? task.ContinueWith<AsyncLock.Releaser>((Func<Task, object, AsyncLock.Releaser>) ((_, state) => new AsyncLock.Releaser((AsyncLock) state)), (object) this, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default) : this.m_releaser;
    }

    public struct Releaser : IDisposable
    {
      private readonly AsyncLock m_toRelease;

      internal Releaser(AsyncLock toRelease) => this.m_toRelease = toRelease;

      public void Dispose()
      {
        if (this.m_toRelease == null)
          return;
        this.m_toRelease.m_semaphore.Release();
      }
    }
  }
}
