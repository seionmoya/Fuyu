using System.Runtime.Serialization;
using Fuyu.Common.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuyu.Tests.Common.Units;

[TestClass]
public class ConfigTest
{
    [TestMethod]
    public void TestConfigService()
    {
        // Before you scream at me why I use GetHashCode let me explain.
        // even if the two object share the same, and ergo the GetHashCode that usually checked between 2 type is not used by the UnitTesting tool.
        // but atleast it works.

        var configservice = ConfigService.GetInstance("test");
        Assert.IsNotNull(configservice);
        var empty_config = new TestConfig();
        var test_config = configservice.GetConfig<TestConfig>("testing");
        Assert.AreEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        test_config.Text = "changedValue";
        configservice.SaveConfig("testing", test_config);
        test_config = configservice.GetConfig<TestConfig>("testing");
        Assert.AreNotEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        Assert.AreEqual(test_config.Text, "changedValue");
        ConfigService.FreeInstance("test");
        configservice = ConfigService.GetInstance("test");
        Assert.IsNotNull(configservice);
        test_config = configservice.GetConfig<TestConfig>("testing");
        Assert.AreNotEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        configservice.DeleteConfig("testing");
        test_config = configservice.GetConfig<TestConfig>("testing");
        Assert.AreEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        ConfigService.FreeInstance("test");
    }

    [TestMethod]
    public void TestConfigServiceLazy()
    {
        // testing with Lazy Dictionary.

        var configservice = ConfigService.GetInstance("lazy");
        Assert.IsNotNull(configservice);

        // first we init both
        var empty_config = new TestConfig();
        var test_config = new TestConfig();

        // getting the lazy and check the same
        configservice.GetConfigLazy("lazy", ref test_config);
        Assert.AreEqual(test_config.GetHashCode(), empty_config.GetHashCode());

        // value change + save.
        test_config.Text = "changedValue";
        configservice.SaveConfigLazy("lazy");

        // we load from cache and see if not the same.
        configservice.GetConfigLazy("lazy", ref test_config);
        Assert.AreNotEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        Assert.AreEqual(test_config.Text, "changedValue");

        // here we want to see if TestConfig2 what will produce
        TestConfig2 testConfig2 = new();
        TestConfig2 testConfig2_empty = new();
        configservice.GetConfigLazy("lazy", ref testConfig2);
        Assert.AreEqual(testConfig2.GetHashCode(), testConfig2_empty.GetHashCode());


        // we reloading the config from disk instead of "cache"
        configservice.GetConfigLazy("lazy", ref test_config, true);

        // check if its the same not the same, but the test_config has prev changed value
        Assert.AreNotEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        Assert.AreEqual(test_config.Text, "changedValue");

        // freeing instance (if we have get we should have free like mem de/alloc)
        ConfigService.FreeInstance("lazy");

        // creating again because we want to see if the same and not only cache
        configservice = ConfigService.GetInstance("lazy");
        Assert.IsNotNull(configservice);

        // again get the config and check if not the same as empty
        configservice.GetConfigLazy("lazy", ref test_config);
        Assert.AreNotEqual(test_config.GetHashCode(), empty_config.GetHashCode());

        // deleting config inside lazy and file too!
        configservice.DeleteConfigLazy("lazy");

        // we check it must be same as before.
        configservice.GetConfigLazy("lazy", ref test_config);
        Assert.AreEqual(test_config.GetHashCode(), empty_config.GetHashCode());
        ConfigService.FreeInstance("lazy");
    }

    [DataContract]
    public class TestConfig : AbstractConfig
    {
        [DataMember]
        public string Text { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(Text))
                return Text.GetHashCode();
            return 0;
        }
    }

    [DataContract]
    public class TestConfig2 : AbstractConfig
    {
        [DataMember]
        public ulong someId { get; set; } = 0;

        public override int GetHashCode()
        {
            return someId.GetHashCode();
        }
    }
}
