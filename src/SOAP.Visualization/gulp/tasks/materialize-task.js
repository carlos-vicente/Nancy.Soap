var gulp = require('gulp');

gulp.task('copyMaterialize', function(){
    gulp.src('bower_components/materialize/dist/**/*.*')
        .pipe(gulp.dest('lib/materialize/'));
});