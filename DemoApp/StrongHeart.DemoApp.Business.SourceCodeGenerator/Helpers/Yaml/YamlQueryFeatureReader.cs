using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Yaml
{
    public class YamlQueryFeatureReader : IFeatureReader<QueryFeatures>
    {
        public QueryFeatures GetFeatures(IEnumerable<AdditionalText> additionalFiles)
        {
            var path = additionalFiles.GetFileContent(x => x.Path.EndsWith("queries.yaml"), text => text.Path)!;
            string[] lines = File.ReadAllLines(path);
            string ns = GetNameSpace(lines);
            var q = GetQueries(lines);

            return new QueryFeatures()
            {
                RootNamespace = ns,
                Items = q.ToArray()
            };
        }

        private string GetNameSpace(string[] lines)
        {
            return lines.First().Replace("- rootNameSpace: ", string.Empty).Trim();
        }

        private IEnumerable<QueryFeature> GetQueries(IList<string> lines)
        {
            lines = lines.Skip(1).ToArray();

            QueryFeature feature = null;
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (line.Contains("- name:"))
                {
                    feature = new QueryFeature()
                    {
                        Response = new QueryResponse(),
                        Request = new QueryRequest()
                    };
                    feature.Name = line.Replace("- name:", string.Empty).Trim();
                }
                else if (line.Contains("requestProperties"))
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
                                feature.Request.Properties = list;
                                line = innerLine;
                                break;
                            }
                        }
                        else { break; }
                    }
                }
                else if (line.Contains("isListResponse: "))
                {
                    feature.Response.IsListResponse = bool.Parse(line.Replace("isListResponse: ", string.Empty).Trim());
                }
                if (line.Contains("responseTypeName:"))
                {
                    feature.Response.ResponseTypeName = line.Replace("responseTypeName:", string.Empty).Trim();
                }
                else if (line.Contains("responseProperties"))
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
                                feature.Response.Properties = list;
                                line = innerLine;
                                break;
                            }
                        }
                        else
                        {
                            feature.Response.Properties = list;
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