var yeoman = require('yeoman-environment');
const gulp = require('gulp');
const rename = require('gulp-rename');
const replace = require('gulp-replace');
const clean = require('gulp-clean');

const fs = require('fs');
const argv = require('yargs').argv;
const { exec, execSync } = require('child_process');

const templatesFolderPath = 'generators/templates';
const distFolderPath = 'dist';
const sourceFolderPath = 'src';
const secretFilePath = './secret.json';

const defaultSolutionName = 'HCA';

gulp.task('clean:templates', () => {
    return gulp.src(`./${templatesFolderPath}`, { allowEmpty: true, read: false }).pipe(clean());
});

gulp.task('copy:src', () => {
    return gulp.src('../src/**')
        .pipe(replace(/HCA/g, '<%= solutionName %>'))
        .pipe(rename((path) => {
            path.basename = path.basename.replace(/HCA/g, 'SolutionName');
            path.dirname = path.dirname.replace(/HCA/g, 'SolutionName')
        }))
        .pipe(gulp.dest(`${templatesFolderPath}/${sourceFolderPath}`));
});

gulp.task('copy:root', () => {
    return gulp.src(['../readme.md', '../LICENSE'])
        .pipe(gulp.dest(`${templatesFolderPath}`));
});

// Npm link approach 
gulp.task('run:yo:linked', () => {
    const solutionName = argv.solutionName || defaultSolutionName;
    fs.mkdirSync(distFolderPath, { recursive: true });
    process.chdir(distFolderPath);
    return exec(`yo hca --solutionName ${solutionName}`);
});

// Correct env-based approach
gulp.task('run:yo', () => {
    const solutionName = argv.solutionName;
    var env = yeoman.createEnv([], { cwd: 'dist' });
    env.register('./generators/app/index', 'hca:app');
    return env.run('hca:app', { solutionName });
});

gulp.task('clean:dist', () => {
    return gulp.src(`./${distFolderPath}`, { allowEmpty: true, read: false }).pipe(clean());
});

gulp.task('publish', () => {
    let secret = fs.existsSync(secretFilePath) ? require(secretFilePath) : {};
    secret = secret.authToken || argv.authToken;
    const registryEntry = `//registry.npmjs.org/:_authToken=${authToken}`;
    fs.writeFileSync('.npmrc', registryEntry, { encoding: 'ASCII' });
    return exec('npm publish');
})

gulp.task('default', gulp.series('clean:templates', gulp.parallel('copy:root', 'copy:src')));

gulp.task('default:generator', gulp.series('clean:dist', 'run:yo'))