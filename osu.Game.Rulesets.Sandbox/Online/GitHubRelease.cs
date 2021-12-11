﻿using Newtonsoft.Json;

namespace osu.Game.Rulesets.Sandbox.Online
{
    public class GitHubRelease
    {
        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
