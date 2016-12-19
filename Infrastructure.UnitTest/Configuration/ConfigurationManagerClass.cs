﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Infrastructure.Configuration;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Xunit;

    public class ConfigurationManagerClass
    {
        private readonly IFixture fixture;

        public ConfigurationManagerClass()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
        }

        public class GetSettingMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldLoadConfigFromFileOnFirstCall()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldOnlyLoadFromFileOnceOnMultipleCalls()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(fixture.Create<string>());
                instance.GetSetting<string>(fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldReturnValueIfExistsAndTypesMatch()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                var value = fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, value));

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(value);
            }

            [Fact]
            public void ShouldReturnDefaultValueForNonExistentSetting()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                if (modConfiguration.Settings.Any(x => x.Key == key))
                {
                    modConfiguration.Settings.Remove(modConfiguration.Settings.Single(x => x.Key == key));
                }

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(default(string));
            }

            [Fact]
            public void ShouldThrowInvalidCastExceptionWhenTryingToCastToInvalidType()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, "myValue"));

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.Invoking(x => x.GetSetting<int>(key)).ShouldThrow<InvalidCastException>().WithMessage("Tried to cast value 'myValue' of type string to Int32.");
            }
        }

        public class MigrateTypeMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldLoadConfigFromDiscOnFirstCall()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingNotFound()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingIsOfCorrectType()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), fixture.Create<int>());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldChangeTypeOfValue()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var intValue = fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), intValue.ToString());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                modConfig.Settings.Should().Contain(x => x.Key == keyValuePair.Key && (int)x.Value == intValue);
            }

            [Fact]
            public void ShouldSaveSetting()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var intValue = fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), intValue.ToString());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Once);
            }
        }
    }
}