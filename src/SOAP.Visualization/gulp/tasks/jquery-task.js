var gulp = require('gulp');

// This task makes sure that when running gul
gulp.task('copyJquery', function(){
    gulp.src('bower_components/jquery/dist/*.js')
        .pipe(gulp.dest('lib/jquery'));
});