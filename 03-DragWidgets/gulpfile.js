/// <binding BeforeBuild='build' />
const { series, parallel } = require('gulp');

var path = require('path'),
    gulp = require('gulp'),
    gp_clean = require('gulp-clean'),
    gp_sass = require('gulp-sass')(require("sass")),
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require("gulp-uglify"),
    buffer = require('vinyl-buffer'),
    source = require('vinyl-source-stream'),
    rename = require("gulp-rename"),
    browserify = require("browserify"),
    tSt = require("gulp-typescript");


const basePath = path.resolve(__dirname, "wwwroot");
const modulePath = path.resolve(__dirname, "node_modules");

const tsProject = tSt.createProject(path.resolve(basePath, 'tsconfig.json'));

var srcPaths = {
    lib: [
        {
            src: path.resolve(modulePath, 'bootstrap/dist/**/*'),
            dest: path.resolve(basePath, 'lib/bootstrap/')
        },
        {
            src: path.resolve(modulePath, '@fortawesome/fontawesome-free/**/*'),
            dest: path.resolve(basePath, 'lib/fontawesome/')
        }
    ],
    srcJs: [
        path.resolve(basePath, 'src/dashboard.js')
    ],
    srcSass: [
        path.resolve(basePath, 'scss/site.scss')
    ]
};

var destPaths = {
    publicCss: path.resolve(basePath, 'css'),
    publicJs: path.resolve(basePath, 'js')
};


function testTask(done) {
    console.log('Hello World! We finished a task!');
    done();
}

/* Tasks */

/* Copy Libraries to their location */
const copyLibraries = (done) => {
    srcPaths.lib.map(item => {
        return gulp.src(item.src)
            .pipe(gulp.dest(item.dest));
    });
    done();
}

const cleanLibraries = (done) => {
    srcPaths.lib.map(item => {
        return gulp.src(item.dest + '**')
            .pipe(gp_clean());
    });
    done();
}

/* TypeScript */
const ts_transpile = (done) => {
    return tsProject.src()
        .pipe(tsProject())
        .pipe(gulp.dest(path.resolve(basePath, 'src')));
}

const ts_clean = (done) => {
    if (srcPaths.srcJs.length > 0) {
        gulp.src(srcPaths.srcJs, { allowEmpty: true })
            .pipe(gp_clean({ force: true }));
    }
    done();
}

/* JavaScript */
const js_bundle_min = (done) => {
    srcPaths.srcJs.forEach(file => {

        const b = browserify({
            entries: file, // Only need initial file, browserify finds the deps
            debug: true, // Enable sourcemaps
            transform: [['babelify', { 'presets': ["@babel/preset-env", "@babel/preset-react"] }]]
        });

        b.bundle()
            .pipe(source(path.basename(file)))
            .pipe(rename(path => {
                path.basename += ".min";
                path.extname = ".js";
            }))
            .pipe(buffer())
            .pipe(sourcemaps.init({ loadMaps: true }))
            .pipe(uglify())
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(destPaths.publicJs));

        done();
    });
}

const js_clean = (done) => {
    gulp.src(path.resolve(destPaths.publicJs, '**/*.js'), { read: false })
        .pipe(gp_clean({ force: true }));
    done();
}


/* SASS/CSS */
const sass_clean = (done) => {
    gulp.src(destPaths.publicCss + "**/*", { read: false })
        .pipe(gp_clean({ force: true }));
    done();
}

const sass_build = (done) => {
    gulp.src(srcPaths.srcSass)
        .pipe(gp_sass({ outputStyle: 'compressed' }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest(destPaths.publicCss));
    done();
}

exports.build = series(
    parallel(
        copyLibraries,
        series(
            ts_transpile,
            js_bundle_min
        ),
        sass_build
    )
);

exports.clean = series(
    cleanLibraries,
    sass_clean,
    ts_clean,
    js_clean
);
