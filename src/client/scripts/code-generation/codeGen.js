var gulp = require("gulp");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var codeGen = require('rainbow-js-codegeneration').generationPlugin;

gulp.task("generate-ts", function (callback) {
  gulp.src('../../**/codegenerationts.config.js', { base: "./" })
      .pipe(foreach(function (stream, file) {
        console.log("Processing (TypeScript) " + file.path);
        return stream;
      }))
      .pipe(codeGen())
      .pipe(rename(function (path) {
          path.basename = "Models.Generated";
          path.extname = ".ts"
      }))
      .pipe(gulp.dest('./'))
      .on("end", function () { 
          // will make sure that you wait for generation to finish
          callback();
      });
});

gulp.task("generate-ts-commerce", function (callback) {
  gulp.src('../../**/codegenerationts.commerce.config.js', { base: "./" })
      .pipe(foreach(function (stream, file) {
        console.log("Processing (TypeScript) " + file.path);
        return stream;
      }))
      .pipe(codeGen())
      .pipe(rename(function (path) {
          path.basename = "Commerce.Generated";
          path.extname = ".ts"
      }))
      .pipe(gulp.dest('./'))
      .on("end", function () { 
          // will make sure that you wait for generation to finish
          callback();
      });
});

gulp.task("default", gulp.series("generate-ts", "generate-ts-commerce"));
