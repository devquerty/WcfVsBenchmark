using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess;

namespace WcfVsWebApiVsAspNetCoreBenchmark
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<WcfVsWebApiVsAspNetCoreMvc>();
        }
    }

    [Config(typeof(Config))]
    public class WcfVsWebApiVsAspNetCoreMvc
    {
        private class Config: ManualConfig
        {
            public Config()
            {
                Add(Job.Clr.With(InProcessToolchain.Instance));
                Add(ConsoleLogger.Default);
                Add(TargetMethodColumn.Method, new ParamColumn(nameof(ItemCount)), StatisticColumn.Mean, StatisticColumn.P95);
                Add(MemoryDiagnoser.Default.GetColumnProvider());
                Add(EnvironmentAnalyser.Default);
                Add(MemoryDiagnoser.Default);
                Add(MarkdownExporter.Default);
                UnionRule = ConfigUnionRule.AlwaysUseLocal;
            }
        }

        [Params(0, 10, 100, 1000)]
        public int ItemCount { get; set; }

        [GlobalSetup]
        public void Initialize()
        {
            int port = 9001;

            _smallTextWcfService = new WcfService<SmallItem>(
                port: port++,
                bindingType: WcfBindingType.BasicText,
                itemCount: ItemCount
            );
            _smallTextWcfService.Start();

            _smallWebXmlWcfService = new WcfService<SmallItem>(
                port: port++,
                bindingType: WcfBindingType.WebXml,
                itemCount: ItemCount
            );
            _smallWebXmlWcfService.Start();

            _smallWebJsonWcfService = new WcfService<SmallItem>(
                port: port++,
                bindingType: WcfBindingType.WebJson,
                itemCount: ItemCount
            );
            _smallWebJsonWcfService.Start();

            _smallBinaryWcfService = new WcfService<SmallItem>(
                port: port++,
                bindingType: WcfBindingType.BinaryMessageEncoding,
                itemCount: ItemCount
            );
            _smallBinaryWcfService.Start();

            _smallNetTcpWcfService = new WcfService<SmallItem>(
                port: port++,
                bindingType: WcfBindingType.NetTcp,
                itemCount: ItemCount
            );
            _smallNetTcpWcfService.Start();

            _smallWebApiJsonNetSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "JsonNet",
                host: "WebApi"
            );
            _smallWebApiJsonNetSuite.Start();

            _smallWebApiMessagePackSuite = new RestBenchmarkSuite<MessagePackSmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "MessagePack",
                host: "WebApi"
            );
            _smallWebApiMessagePackSuite.Start();

            _smallWebApiXmlSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Xml",
                host: "WebApi"
            );
            _smallWebApiXmlSuite.Start();

            _smallWebApiUtf8JsonSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Utf8Json",
                host: "WebApi"
            );
            _smallWebApiUtf8JsonSuite.Start();

            _smallWebApiZeroFormatterSuite = new RestBenchmarkSuite<ZeroFormatterSmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "ZeroFormatter",
                host: "WebApi"
            );
            _smallWebApiZeroFormatterSuite.Start();

            _smallAspNetCoreJsonNetSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "JsonNet",
                host: "AspNetCore"
            );
            _smallAspNetCoreJsonNetSuite.Start();

            _smallAspNetCoreMessagePackSuite = new RestBenchmarkSuite<MessagePackSmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "MessagePack",
                host: "AspNetCore"
            );
            _smallAspNetCoreMessagePackSuite.Start();

            _smallAspNetCoreXmlSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Xml",
                host: "AspNetCore"
            );
            _smallAspNetCoreXmlSuite.Start();

            _smallAspNetCoreUtf8JsonSuite = new RestBenchmarkSuite<SmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Utf8Json",
                host: "AspNetCore"
            );
            _smallAspNetCoreUtf8JsonSuite.Start();

            _smallAspNetCoreZeroFormatterSuite = new RestBenchmarkSuite<ZeroFormatterSmallItem>(
                port: port++,
                itemCount: ItemCount,
                format: "ZeroFormatter",
                host: "AspNetCore"
            );
            _smallAspNetCoreZeroFormatterSuite.Start();

            _largeTextWcfService = new WcfService<LargeItem>(
                port: port++,
                bindingType: WcfBindingType.BasicText,
                itemCount: ItemCount
            );
            _largeTextWcfService.Start();

            _largeWebXmlWcfService = new WcfService<LargeItem>(
                port: port++,
                bindingType: WcfBindingType.WebXml,
                itemCount: ItemCount
            );
            _largeWebXmlWcfService.Start();

            _largeWebJsonWcfService = new WcfService<LargeItem>(
                port: port++,
                bindingType: WcfBindingType.WebJson,
                itemCount: ItemCount
            );
            _largeWebJsonWcfService.Start();

            _largeBinaryWcfService = new WcfService<LargeItem>(
                port: port++,
                bindingType: WcfBindingType.BinaryMessageEncoding,
                itemCount: ItemCount
            );
            _largeBinaryWcfService.Start();

            _largeNetTcpWcfService = new WcfService<LargeItem>(
                port: port++,
                bindingType: WcfBindingType.NetTcp,
                itemCount: ItemCount
            );
            _largeNetTcpWcfService.Start();

            _largeWebApiJsonNetSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "JsonNet",
                host: "WebApi"
            );
            _largeWebApiJsonNetSuite.Start();

            _largeWebApiMessagePackSuite = new RestBenchmarkSuite<MessagePackLargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "MessagePack",
                host: "WebApi"
            );
            _largeWebApiMessagePackSuite.Start();

            _largeWebApiXmlSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Xml",
                host: "WebApi"
            );
            _largeWebApiXmlSuite.Start();
            
            _largeWebApiUtf8JsonSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Utf8Json",
                host: "WebApi"
            );
            _largeWebApiUtf8JsonSuite.Start();
            
            _largeWebApiZeroFormatterSuite = new RestBenchmarkSuite<ZeroFormatterLargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "ZeroFormatter",
                host: "WebApi"
            );
            _largeWebApiZeroFormatterSuite.Start();

            _largeAspNetCoreJsonNetSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "JsonNet",
                host: "AspNetCore"
            );
            _largeAspNetCoreJsonNetSuite.Start();

            _largeAspNetCoreMessagePackSuite = new RestBenchmarkSuite<MessagePackLargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "MessagePack",
                host: "AspNetCore"
            );
            _largeAspNetCoreMessagePackSuite.Start();

            _largeAspNetCoreXmlSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Xml",
                host: "AspNetCore"
            );
            _largeAspNetCoreXmlSuite.Start();

            _largeAspNetCoreUtf8JsonSuite = new RestBenchmarkSuite<LargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "Utf8Json",
                host: "AspNetCore"
            );
            _largeAspNetCoreUtf8JsonSuite.Start();

            _largeAspNetCoreZeroFormatterSuite = new RestBenchmarkSuite<ZeroFormatterLargeItem>(
                port: port++,
                itemCount: ItemCount,
                format: "ZeroFormatter",
                host: "AspNetCore"
            );
            _largeAspNetCoreZeroFormatterSuite.Start();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _smallTextWcfService?.Stop();
            _smallWebXmlWcfService?.Stop();
            _smallWebJsonWcfService?.Stop();
            _smallBinaryWcfService?.Stop();
            _smallNetTcpWcfService?.Stop();
            _smallWebApiJsonNetSuite?.Stop();
            _smallWebApiMessagePackSuite?.Stop();
            _smallWebApiXmlSuite?.Stop();
            _smallWebApiUtf8JsonSuite?.Stop();
            _smallWebApiZeroFormatterSuite?.Stop();
            _smallAspNetCoreJsonNetSuite?.Stop();
            _smallAspNetCoreMessagePackSuite?.Stop();
            _smallAspNetCoreXmlSuite?.Stop();
            _smallAspNetCoreUtf8JsonSuite?.Stop();
            _smallAspNetCoreZeroFormatterSuite?.Stop();
            _largeTextWcfService?.Stop();
            _largeWebXmlWcfService?.Stop();
            _largeWebJsonWcfService?.Stop();
            _largeBinaryWcfService?.Stop();
            _largeNetTcpWcfService?.Stop();
            _largeWebApiJsonNetSuite?.Stop();
            _largeWebApiMessagePackSuite?.Stop();
            _largeWebApiXmlSuite?.Stop();
            _largeWebApiUtf8JsonSuite?.Stop();
            _largeWebApiZeroFormatterSuite?.Stop();
            _largeAspNetCoreJsonNetSuite?.Stop();
            _largeAspNetCoreMessagePackSuite?.Stop();
            _largeAspNetCoreXmlSuite?.Stop();
            _largeAspNetCoreUtf8JsonSuite?.Stop();
            _largeAspNetCoreZeroFormatterSuite?.Stop();
        }

        public void InvokeAll()
        {
            foreach(var method in typeof(WcfVsWebApiVsAspNetCoreMvc)
                                 .GetMethods()
                                 .Where(m => Attribute.GetCustomAttribute(m, typeof(BenchmarkAttribute)) != null))
            {
                method.Invoke(this, new object[0]);
                Console.WriteLine($"{method.Name}: OK");
            }
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWcfText()
        {
            return _smallTextWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWcfWebXml()
        {
            return _smallWebXmlWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWcfWebJson()
        {
            return _smallWebJsonWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWcfBinary()
        {
            return _smallBinaryWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWcfNetTcp()
        {
            return _smallNetTcpWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiJsonNetHttpClient()
        {
            return _smallWebApiJsonNetSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallWebApiJsonNetHttpClientAsync()
        {
            return _smallWebApiJsonNetSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiJsonNetHttpWebRequest()
        {
            return _smallWebApiJsonNetSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiJsonNetHttpClient()
        {
            return _smallWebApiJsonNetSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedWebApiJsonNetHttpClientAsync()
        {
            return _smallWebApiJsonNetSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiJsonNetHttpWebRequest()
        {
            return _smallWebApiJsonNetSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallWebApiMessagePackHttpClient()
        {
            return _smallWebApiMessagePackSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackSmallItem>> SmallWebApiMessagePackHttpClientAsync()
        {
            return _smallWebApiMessagePackSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallWebApiMessagePackHttpWebRequest()
        {
            return _smallWebApiMessagePackSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallPrecomputedWebApiMessagePackHttpClient()
        {
            return _smallWebApiMessagePackSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackSmallItem>> SmallPrecomputedWebApiMessagePackHttpClientAsync()
        {
            return _smallWebApiMessagePackSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallPrecomputedWebApiMessagePackHttpWebRequest()
        {
            return _smallWebApiMessagePackSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiXmlHttpClient()
        {
            return _smallWebApiXmlSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallWebApiXmlHttpClientAsync()
        {
            return _smallWebApiXmlSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiXmlHttpWebRequest()
        {
            return _smallWebApiXmlSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiXmlHttpClient()
        {
            return _smallWebApiXmlSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedWebApiXmlHttpClientAsync()
        {
            return _smallWebApiXmlSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiXmlHttpWebRequest()
        {
            return _smallWebApiXmlSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiUtf8JsonHttpClient()
        {
            return _smallWebApiUtf8JsonSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallWebApiUtf8JsonHttpClientAsync()
        {
            return _smallWebApiUtf8JsonSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallWebApiUtf8JsonHttpWebRequest()
        {
            return _smallWebApiUtf8JsonSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiUtf8JsonHttpClient()
        {
            return _smallWebApiUtf8JsonSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedWebApiUtf8JsonHttpClientAsync()
        {
            return _smallWebApiUtf8JsonSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedWebApiUtf8JsonHttpWebRequest()
        {
            return _smallWebApiUtf8JsonSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallWebApiZeroFormatterHttpClient()
        {
            return _smallWebApiZeroFormatterSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterSmallItem>> SmallWebApiZeroFormatterHttpClientAsync()
        {
            return _smallWebApiZeroFormatterSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallWebApiZeroFormatterHttpWebRequest()
        {
            return _smallWebApiZeroFormatterSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallPrecomputedWebApiZeroFormatterHttpClient()
        {
            return _smallWebApiZeroFormatterSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterSmallItem>> SmallPrecomputedWebApiZeroFormatterHttpClientAsync()
        {
            return _smallWebApiZeroFormatterSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallPrecomputedWebApiZeroFormatterHttpWebRequest()
        {
            return _smallWebApiZeroFormatterSuite.InvokePrecomputedHttpWebRequest();
        }
        
        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreJsonNetHttpClient()
        {
            return _smallAspNetCoreJsonNetSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallAspNetCoreJsonNetHttpClientAsync()
        {
            return _smallAspNetCoreJsonNetSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreJsonNetHttpWebRequest()
        {
            return _smallAspNetCoreJsonNetSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreJsonNetHttpClient()
        {
            return _smallAspNetCoreJsonNetSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedAspNetCoreJsonNetHttpClientAsync()
        {
            return _smallAspNetCoreJsonNetSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreJsonNetHttpWebRequest()
        {
            return _smallAspNetCoreJsonNetSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallAspNetCoreMessagePackHttpClient()
        {
            return _smallAspNetCoreMessagePackSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackSmallItem>> SmallAspNetCoreMessagePackHttpClientAsync()
        {
            return _smallAspNetCoreMessagePackSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallAspNetCoreMessagePackHttpWebRequest()
        {
            return _smallAspNetCoreMessagePackSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallPrecomputedAspNetCoreMessagePackHttpClient()
        {
            return _smallAspNetCoreMessagePackSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackSmallItem>> SmallPrecomputedAspNetCoreMessagePackHttpClientAsync()
        {
            return _smallAspNetCoreMessagePackSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackSmallItem> SmallPrecomputedAspNetCoreMessagePackHttpWebRequest()
        {
            return _smallAspNetCoreMessagePackSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreXmlHttpClient()
        {
            return _smallAspNetCoreXmlSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallAspNetCoreXmlHttpClientAsync()
        {
            return _smallAspNetCoreXmlSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreXmlHttpWebRequest()
        {
            return _smallAspNetCoreXmlSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreXmlHttpClient()
        {
            return _smallAspNetCoreXmlSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedAspNetCoreXmlHttpClientAsync()
        {
            return _smallAspNetCoreXmlSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreXmlHttpWebRequest()
        {
            return _smallAspNetCoreXmlSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreUtf8JsonHttpClient()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallAspNetCoreUtf8JsonHttpClientAsync()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallAspNetCoreUtf8JsonHttpWebRequest()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreUtf8JsonHttpClient()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<SmallItem>> SmallPrecomputedAspNetCoreUtf8JsonHttpClientAsync()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<SmallItem> SmallPrecomputedAspNetCoreUtf8JsonHttpWebRequest()
        {
            return _smallAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpWebRequest();
        }
        
        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallAspNetCoreZeroFormatterHttpClient()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterSmallItem>> SmallAspNetCoreZeroFormatterHttpClientAsync()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallAspNetCoreZeroFormatterHttpWebRequest()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallPrecomputedAspNetCoreZeroFormatterHttpClient()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterSmallItem>> SmallPrecomputedAspNetCoreZeroFormatterHttpClientAsync()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterSmallItem> SmallPrecomputedAspNetCoreZeroFormatterHttpWebRequest()
        {
            return _smallAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWcfText()
        {
            return _largeTextWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWcfWebXml()
        {
            return _largeWebXmlWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWcfWebJson()
        {
            return _largeWebJsonWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWcfBinary()
        {
            return _largeBinaryWcfService.Invoke();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWcfNetTcp()
        {
            return _largeNetTcpWcfService.Invoke();
        }
        
        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiJsonNetHttpClient()
        {
            return _largeWebApiJsonNetSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeWebApiJsonNetHttpClientAsync()
        {
            return _largeWebApiJsonNetSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiJsonNetHttpWebRequest()
        {
            return _largeWebApiJsonNetSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiJsonNetHttpClient()
        {
            return _largeWebApiJsonNetSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedWebApiJsonNetHttpClientAsync()
        {
            return _largeWebApiJsonNetSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiJsonNetHttpWebRequest()
        {
            return _largeWebApiJsonNetSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargeWebApiMessagePackHttpClient()
        {
            return _largeWebApiMessagePackSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackLargeItem>> LargeWebApiMessagePackHttpClientAsync()
        {
            return _largeWebApiMessagePackSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargeWebApiMessagePackHttpWebRequest()
        {
            return _largeWebApiMessagePackSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargePrecomputedWebApiMessagePackHttpClient()
        {
            return _largeWebApiMessagePackSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackLargeItem>> LargePrecomputedWebApiMessagePackHttpClientAsync()
        {
            return _largeWebApiMessagePackSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargePrecomputedWebApiMessagePackHttpWebRequest()
        {
            return _largeWebApiMessagePackSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiXmlHttpClient()
        {
            return _largeWebApiXmlSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeWebApiXmlHttpClientAsync()
        {
            return _largeWebApiXmlSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiXmlHttpWebRequest()
        {
            return _largeWebApiXmlSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiXmlHttpClient()
        {
            return _largeWebApiXmlSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedWebApiXmlHttpClientAsync()
        {
            return _largeWebApiXmlSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiXmlHttpWebRequest()
        {
            return _largeWebApiXmlSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiUtf8JsonHttpClient()
        {
            return _largeWebApiUtf8JsonSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeWebApiUtf8JsonHttpClientAsync()
        {
            return _largeWebApiUtf8JsonSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeWebApiUtf8JsonHttpWebRequest()
        {
            return _largeWebApiUtf8JsonSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiUtf8JsonHttpClient()
        {
            return _largeWebApiUtf8JsonSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedWebApiUtf8JsonHttpClientAsync()
        {
            return _largeWebApiUtf8JsonSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedWebApiUtf8JsonHttpWebRequest()
        {
            return _largeWebApiUtf8JsonSuite.InvokePrecomputedHttpWebRequest();
        }
        
        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargeWebApiZeroFormatterHttpClient()
        {
            return _largeWebApiZeroFormatterSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterLargeItem>> LargeWebApiZeroFormatterHttpClientAsync()
        {
            return _largeWebApiZeroFormatterSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargeWebApiZeroFormatterHttpWebRequest()
        {
            return _largeWebApiZeroFormatterSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargePrecomputedWebApiZeroFormatterHttpClient()
        {
            return _largeWebApiZeroFormatterSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterLargeItem>> LargePrecomputedWebApiZeroFormatterHttpClientAsync()
        {
            return _largeWebApiZeroFormatterSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargePrecomputedWebApiZeroFormatterHttpWebRequest()
        {
            return _largeWebApiZeroFormatterSuite.InvokePrecomputedHttpWebRequest();
        }
        
        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreJsonNetHttpClient()
        {
            return _largeAspNetCoreJsonNetSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeAspNetCoreJsonNetHttpClientAsync()
        {
            return _largeAspNetCoreJsonNetSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreJsonNetHttpWebRequest()
        {
            return _largeAspNetCoreJsonNetSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreJsonNetHttpClient()
        {
            return _largeAspNetCoreJsonNetSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedAspNetCoreJsonNetHttpClientAsync()
        {
            return _largeAspNetCoreJsonNetSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreJsonNetHttpWebRequest()
        {
            return _largeAspNetCoreJsonNetSuite.InvokePrecomputedHttpWebRequest();
        }
        
        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargeAspNetCoreMessagePackHttpClient()
        {
            return _largeAspNetCoreMessagePackSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackLargeItem>> LargeAspNetCoreMessagePackHttpClientAsync()
        {
            return _largeAspNetCoreMessagePackSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargeAspNetCoreMessagePackHttpWebRequest()
        {
            return _largeAspNetCoreMessagePackSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargePrecomputedAspNetCoreMessagePackHttpClient()
        {
            return _largeAspNetCoreMessagePackSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<MessagePackLargeItem>> LargePrecomputedAspNetCoreMessagePackHttpClientAsync()
        {
            return _largeAspNetCoreMessagePackSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<MessagePackLargeItem> LargePrecomputedAspNetCoreMessagePackHttpWebRequest()
        {
            return _largeAspNetCoreMessagePackSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreXmlHttpClient()
        {
            return _largeAspNetCoreXmlSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeAspNetCoreXmlHttpClientAsync()
        {
            return _largeAspNetCoreXmlSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreXmlHttpWebRequest()
        {
            return _largeAspNetCoreXmlSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreXmlHttpClient()
        {
            return _largeAspNetCoreXmlSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedAspNetCoreXmlHttpClientAsync()
        {
            return _largeAspNetCoreXmlSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreXmlHttpWebRequest()
        {
            return _largeAspNetCoreXmlSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreUtf8JsonHttpClient()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargeAspNetCoreUtf8JsonHttpClientAsync()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargeAspNetCoreUtf8JsonHttpWebRequest()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreUtf8JsonHttpClient()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<LargeItem>> LargePrecomputedAspNetCoreUtf8JsonHttpClientAsync()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<LargeItem> LargePrecomputedAspNetCoreUtf8JsonHttpWebRequest()
        {
            return _largeAspNetCoreUtf8JsonSuite.InvokePrecomputedHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargeAspNetCoreZeroFormatterHttpClient()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokeHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterLargeItem>> LargeAspNetCoreZeroFormatterHttpClientAsync()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokeHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargeAspNetCoreZeroFormatterHttpWebRequest()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokeHttpWebRequest();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargePrecomputedAspNetCoreZeroFormatterHttpClient()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpClient();
        }

        [Benchmark]
        public Task<IReadOnlyCollection<ZeroFormatterLargeItem>> LargePrecomputedAspNetCoreZeroFormatterHttpClientAsync()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpClientAsync();
        }

        [Benchmark]
        public IReadOnlyCollection<ZeroFormatterLargeItem> LargePrecomputedAspNetCoreZeroFormatterHttpWebRequest()
        {
            return _largeAspNetCoreZeroFormatterSuite.InvokePrecomputedHttpWebRequest();
        }

        private WcfService<SmallItem> _smallTextWcfService;
        private WcfService<SmallItem> _smallWebXmlWcfService;
        private WcfService<SmallItem> _smallWebJsonWcfService;
        private WcfService<SmallItem> _smallBinaryWcfService;
        private WcfService<SmallItem> _smallNetTcpWcfService;
        private RestBenchmarkSuite<SmallItem> _smallWebApiJsonNetSuite;
        private RestBenchmarkSuite<MessagePackSmallItem> _smallWebApiMessagePackSuite;
        private RestBenchmarkSuite<SmallItem> _smallWebApiXmlSuite;
        private RestBenchmarkSuite<SmallItem> _smallWebApiUtf8JsonSuite;
        private RestBenchmarkSuite<ZeroFormatterSmallItem> _smallWebApiZeroFormatterSuite;
        private RestBenchmarkSuite<SmallItem> _smallAspNetCoreJsonNetSuite;
        private RestBenchmarkSuite<MessagePackSmallItem> _smallAspNetCoreMessagePackSuite;
        private RestBenchmarkSuite<SmallItem> _smallAspNetCoreXmlSuite;
        private RestBenchmarkSuite<SmallItem> _smallAspNetCoreUtf8JsonSuite;
        private RestBenchmarkSuite<ZeroFormatterSmallItem> _smallAspNetCoreZeroFormatterSuite;
        private WcfService<LargeItem> _largeTextWcfService;
        private WcfService<LargeItem> _largeWebXmlWcfService;
        private WcfService<LargeItem> _largeWebJsonWcfService;
        private WcfService<LargeItem> _largeBinaryWcfService;
        private WcfService<LargeItem> _largeNetTcpWcfService;
        private RestBenchmarkSuite<LargeItem> _largeWebApiJsonNetSuite;
        private RestBenchmarkSuite<MessagePackLargeItem> _largeWebApiMessagePackSuite;
        private RestBenchmarkSuite<LargeItem> _largeWebApiXmlSuite;
        private RestBenchmarkSuite<LargeItem> _largeWebApiUtf8JsonSuite;
        private RestBenchmarkSuite<ZeroFormatterLargeItem> _largeWebApiZeroFormatterSuite;
        private RestBenchmarkSuite<LargeItem> _largeAspNetCoreJsonNetSuite;
        private RestBenchmarkSuite<MessagePackLargeItem> _largeAspNetCoreMessagePackSuite;
        private RestBenchmarkSuite<LargeItem> _largeAspNetCoreXmlSuite;
        private RestBenchmarkSuite<LargeItem> _largeAspNetCoreUtf8JsonSuite;
        private RestBenchmarkSuite<ZeroFormatterLargeItem> _largeAspNetCoreZeroFormatterSuite;
    }
}
