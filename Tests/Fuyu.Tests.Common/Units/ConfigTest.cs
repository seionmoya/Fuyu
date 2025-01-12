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
        // GetHashCode returns a hash of the value. so we don't compare references
        // Other check if value which is checked after hashcode

        // creating new instance
        var configservice = ConfigService.GetInstance("test");
        Assert.IsNotNull(configservice);

        // creating empty and other empty TestConfig.
        var emptyConfig = new TestConfig();
        var testConfig = configservice.GetConfig<TestConfig>("testing");

        // we are checking if both are the same
        Assert.AreEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreEqual(testConfig.Text, emptyConfig.Text);

        // we changing the value and saving it
        testConfig.Text = "changedValue";
        configservice.SaveConfig("testing", testConfig);
        // we make sure it is not Equal!
        Assert.AreNotEqual(testConfig.Text, emptyConfig.Text);

        // we reload from the config change
        testConfig = configservice.GetConfig<TestConfig>("testing");

        // we make sure the testConfig is not empty!
        Assert.AreNotEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreNotEqual(testConfig.Text, emptyConfig.Text);

        // we make sure the Text is our prev set value.
        Assert.AreEqual(testConfig.Text, "changedValue");

        // freeing and re-getting for test.
        ConfigService.FreeInstance("test");
        configservice = ConfigService.GetInstance("test");
        Assert.IsNotNull(configservice);

        // check if the config from disk not empty
        testConfig = configservice.GetConfig<TestConfig>("testing");
        Assert.AreNotEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreNotEqual(testConfig.Text, emptyConfig.Text);

        // deleting the config
        configservice.DeleteConfig("testing");

        // getting the deleted config.
        testConfig = configservice.GetConfig<TestConfig>("testing");

        // testing if its actually empty.
        Assert.AreEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreEqual(testConfig.Text, emptyConfig.Text);

        // lastly we free (ensuring nothing will use it.)
        ConfigService.FreeInstance("test");
    }

    [TestMethod]
    public void TestConfigServiceLazy()
    {
        // testing with Lazy Dictionary.
        var configservice = ConfigService.GetInstance("lazy");
        Assert.IsNotNull(configservice);

        // first we init both
        var emptyConfig = new TestConfig();
        var testConfig = new TestConfig();

        // getting the lazy and check the same
        configservice.GetConfigLazy("lazy", ref testConfig);
        Assert.AreEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());

        // value change + save.
        testConfig.Text = "changedValue";
        configservice.SaveConfigLazy("lazy");

        // we load from cache and see if not the same.
        configservice.GetConfigLazy("lazy", ref testConfig);
        Assert.AreNotEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreEqual(testConfig.Text, "changedValue");

        // here we want to see if TestConfig2 what will produce
        TestConfig2 testConfig2 = new();
        TestConfig2 testConfig2_empty = new();
        configservice.GetConfigLazy("lazy", ref testConfig2);
        Assert.AreEqual(testConfig2.GetHashCode(), testConfig2_empty.GetHashCode());


        // we reloading the config from disk instead of "cache"
        configservice.GetConfigLazy("lazy", ref testConfig, true);

        // check if its the same not the same, but the testConfig has prev changed value
        Assert.AreNotEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
        Assert.AreEqual(testConfig.Text, "changedValue");

        // freeing instance (if we have get we should have free like mem de/alloc)
        ConfigService.FreeInstance("lazy");

        // creating again because we want to see if the same and not only cache
        configservice = ConfigService.GetInstance("lazy");
        Assert.IsNotNull(configservice);

        // again get the config and check if not the same as empty
        configservice.GetConfigLazy("lazy", ref testConfig);
        Assert.AreNotEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());

        // deleting config inside lazy and file too!
        configservice.DeleteConfigLazy("lazy");

        // we check it must be same as before.
        configservice.GetConfigLazy("lazy", ref testConfig);
        Assert.AreEqual(testConfig.GetHashCode(), emptyConfig.GetHashCode());
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
            {
                return Text.GetHashCode();
            }
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