namespace DotNet.Testcontainers.Core.Models
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;

  internal class TestcontainersConfiguration
  {
    public ContainerConfiguration Container { get; set; } = new ContainerConfiguration();

    public HostConfiguration Host { get; set; } = new HostConfiguration();

    public bool CleanUp { get; set; } = true;

    public TestcontainersConfiguration Merge(TestcontainersConfiguration old)
    {
      this.Container.Image = Merge(this.Container.Image, old.Container.Image);

      this.Container.Name = Merge(this.Container.Name, old.Container.Name);

      this.Container.WorkingDirectory = Merge(this.Container.WorkingDirectory, old.Container.WorkingDirectory);

      this.Container.Entrypoint = Merge(this.Container.Entrypoint, old.Container.Entrypoint);

      this.Container.Command = Merge(this.Container.Command, old.Container.Command);

      this.Container.Environments = Merge(this.Container.Environments, old.Container.Environments);

      this.Container.ExposedPorts = Merge(this.Container.ExposedPorts, old.Container.ExposedPorts);

      this.Container.Labels = Merge(this.Container.Labels, old.Container.Labels);

      this.Host.PortBindings = Merge(this.Host.PortBindings, old.Host.PortBindings);

      this.Host.Mounts = Merge(this.Host.Mounts, old.Host.Mounts);

      return this;
    }

    private static T Merge<T>(T myself, T old)
      where T : class
    {
      return myself ?? old;
    }

    private static IReadOnlyCollection<T> Merge<T>(IReadOnlyCollection<T> myself, ref IReadOnlyCollection<T> old)
      where T : class
    {
      if (myself == null)
      {
        return old ?? new ReadOnlyCollection<T>(new List<T>());
      }
      else
      {
        return myself.Concat(old).ToList();
      }
    }

    private static IReadOnlyDictionary<T, T> Merge<T>(IReadOnlyDictionary<T, T> myself, IReadOnlyDictionary<T, T> old)
      where T : class
    {
      if (myself == null)
      {
        return old ?? new ReadOnlyDictionary<T, T>(new Dictionary<T, T>());
      }
      else
      {
        return myself.Concat(old).ToDictionary(item => item.Key, item => item.Value);
      }
    }

    public class ContainerConfiguration
    {
      public string Image { get; set; }

      public string Name { get; set; }

      public string WorkingDirectory { get; set; }

      public IReadOnlyCollection<string> Entrypoint { get; set; } = new List<string>();

      public IReadOnlyCollection<string> Command { get; set; } = new List<string>();

      public IReadOnlyDictionary<string, string> Environments { get; set; } = new Dictionary<string, string>();

      public IReadOnlyDictionary<string, string> Labels { get; set; } = new Dictionary<string, string>();

      public IReadOnlyDictionary<string, string> ExposedPorts { get; set; } = new Dictionary<string, string>();
    }

    public class HostConfiguration
    {
      public IReadOnlyDictionary<string, string> PortBindings { get; set; } = new Dictionary<string, string>();

      public IReadOnlyDictionary<string, string> Mounts { get; set; } = new Dictionary<string, string>();
    }
  }
}