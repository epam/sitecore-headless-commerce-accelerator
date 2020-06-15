var gulp = require("gulp");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var codeGen = require('rainbow-js-codegeneration').generationPlugin;

gulp.task("generate-cs", function (callback) {
  gulp.src('../../**/codegeneration.config.js', { base: "./" })
      .pipe(foreach(function (stream, file) {
        console.log("Processing (C#) " + file.path);
        return stream;
      }))
      .pipe(codeGen())
      .pipe(rename(function (path) {
          path.basename = "Models.Generated";
          path.extname = ".cs"
      }))
      .pipe(gulp.dest('./'))
      .on("end", function () { 
          // will make sure that you wait for generation to finish
          callback();
      });
});

gulp.task("generate-cs-commerce", function (callback) {
  gulp.src('../../**/codegeneration.commerce.config.js', { base: "./" })
      .pipe(foreach(function (stream, file) {
        console.log("Processing (C#) " + file.path);
        return stream;
      }))
      .pipe(codeGen())
      .pipe(rename(function (path) {
          path.basename = "Commerce.Generated";
          path.extname = ".cs"
      }))
      .pipe(gulp.dest('./'))
      .on("end", function () { 
          // will make sure that you wait for generation to finish
          callback();
      });
});

gulp.task("default", gulp.series("generate-cs", "generate-cs-commerce"));