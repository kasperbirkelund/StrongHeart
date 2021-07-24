﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Yaml
{
    public class YamlCommandFeatureReader : IFeatureReader<CommandFeatures>
    {
        public CommandFeatures GetFeatures(IEnumerable<AdditionalText> additionalFiles)
        {
            var path = additionalFiles.GetFileContent(x => x.Path.EndsWith("commands.yaml"), text => text.Path)!;
            string[] lines = File.ReadAllLines(path);
            string ns = GetNameSpace(lines);
            var q = GetCommands(lines);

            return new CommandFeatures()
            {
                RootNamespace = ns,
                Items = q.ToArray()
            };
        }

        private string GetNameSpace(string[] lines)
        {
            return lines.First().Replace("- rootNameSpace: ", string.Empty).Trim();
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
                    for (++i; ; i++)
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
                    for (++i; ; i++)
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

    }
}