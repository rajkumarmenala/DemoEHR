/// <binding AfterBuild='copy' />
var gulp = require("gulp"), rimraf = require("rimraf"), concat = require("gulp-concat"), cssmin = require("gulp-cssmin"), uglify = require("gulp-uglify"), project = require("./project.json"), del = require('del');
var paths = {
    webroot: "./" + project.webroot + "/",
    lib: "./" + project.webroot + "/lib/",
    app: "./" + project.webroot + "/app/",
    srcapp: "./App/",
    targetapp: "./" + project.webroot + "/App/",
};
paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
gulp.task("clean:js", function(cb) {
    rimraf(paths.concatJsDest, cb);
});
gulp.task("clean:css", function(cb) {
    rimraf(paths.concatCssDest, cb);
});
// Make sure you install del module from NPM as explained , run  $ npm install --save-dev gulp del on command prompt 
//https://github.com/gulpjs/gulp/blob/master/docs/recipes/delete-files-folder.md
gulp.task("min:js", function() {
    gulp.src([paths.js, "!" + paths.minJs], {
        base: "."
    }).pipe(concat(paths.concatJsDest)).pipe(uglify()).pipe(gulp.dest("."));
});
gulp.task("min:css", function() {
    gulp.src([paths.css, "!" + paths.minCss]).pipe(concat(paths.concatCssDest)).pipe(cssmin()).pipe(gulp.dest("."));
});
gulp.task("clean", ["clean:js", "clean:css"], function(cb) {
    del([], cb);
});
gulp.task('copy', function() {
    var filesToMove = ['./App/**/*.js', './App/**/*.html'];
    var contentFilesToMove = ['./Content/**/*.*'];
    gulp.src(filesToMove).pipe(gulp.dest(paths.webroot + "/App/"));
    gulp.src(contentFilesToMove).pipe(gulp.dest(paths.webroot));
});
