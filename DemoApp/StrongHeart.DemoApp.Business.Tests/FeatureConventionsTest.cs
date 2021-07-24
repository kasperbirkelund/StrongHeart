using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace StrongHeart.DemoApp.Business.Tests
{
    public class FeatureConventionsTest
    {
        [Fact]
        public void SingleItemResponseShouldBeImmutable()
        {
            var lines = File.ReadAllLines(
                @"C:\development\azuredevops\StrongHeart\DemoApp\StrongHeart.DemoApp.Business\Features\Commands\commands.yaml");
            var v = GetCommands(lines).ToArray();
        }

        private IEnumerable<CommandFeature> GetCommands(IList<string> lines)
        {
            lines = lines.Skip(1).ToArray();

            CommandFeature feature = null;
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (line.Contains("- name:"))
                {
                    feature = new CommandFeature()
                    {
                        Request = new CommandRequest()
                    };
                    feature.Name = line.Replace("- name:", string.Empty).Trim();
                }
                else if (line.Contains("additionalRequestProperties"))
                {
                    List<string> list = new();
                    for (++i;; i++)
                    {
                        if (i < lines.Count)
                        {
                            string innerLine = lines[i];
                            if (innerLine.TrimStart().StartsWith("-"))
                            {
                                list.Add(innerLine.Replace("-", string.Empty).Trim());
                            }
                            else
                            {
                                feature.Request.AdditionalRequestProperties = list;
                                line = innerLine;
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (line.Contains("dtoProperties"))
                {
                    List<string> list = new();
                    for (++i;; i++)
                    {
                        if (i < lines.Count)
                        {
                            string innerLine = lines[i];
                            if (innerLine.TrimStart().StartsWith("-"))
                            {
                                list.Add(innerLine.Replace("-", string.Empty).Trim());
                            }
                            else
                            {
                                feature.Request.DtoProperties = list;
                                line = innerLine;
                                break;
                            }
                        }
                        else
                        {
                            feature.Request.DtoProperties = list;
                            yield return feature;
                            break;
                        }
                    }
                }

                if (line == string.Empty)
                {
                    yield return feature;
                }
            }
        }


        public class CommandRequest
        {
            public List<string> AdditionalRequestProperties { get; set; }
            public List<string> DtoProperties { get; set; }
        }

        public class CommandFeatures
        {
            public string RootNamespace { get; set; }
            public CommandFeature[] Items { get; set; }
        }

        public class CommandFeature
        {
            public string Name { get; set; }
            public CommandRequest Request { get; set; }
        }
    }
}