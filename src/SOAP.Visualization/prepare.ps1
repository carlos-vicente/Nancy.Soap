Write-Host "Going to install NPM packages"

npm install

Write-Host "Going to install Bower packages"

./node_modules/.bin/bower install

Write-Host "Going to execute gulp default task"

./node_modules/.bin/gulp