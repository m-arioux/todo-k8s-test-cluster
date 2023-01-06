const express = require("express");
const { stringReplace } = require("string-replace-middleware");
const path = require("path");
const fs = require("fs");

const app = express();

const apiBaseUrlEnvKey = "API_BASE_URL";
const apiBaseUrl = process.env[apiBaseUrlEnvKey];
if ((apiBaseUrl || "").trim().length === 0) {
  throw new Error(`missing environment variable ${apiBaseUrlEnvKey}`);
}

app.get("/", (req, res) => {
  fs.readFile(path.join(__dirname, "/public/index.html"), (err, data) => {
    const content = data
      .toString()
      .replace(`{{${apiBaseUrlEnvKey}}}`, apiBaseUrl);
    res.contentType("html");
    res.send(content);
  });
});

app.use(
  stringReplace(
    { "{{API_BASE_URL}}": apiBaseUrl },
    { contentTypeFilterRegexp: /.*/ }
  )
);

app.listen(5000);
