module.exports = {
  // all paths below should be assigned relative to client root
  rootDir: './../../',
  coverageReporters: ['json', 'html', 'cobertura', 'text-summary'],
  // this path is duplicated in the build.cake for xUnit tests
  coverageDirectory: './../../output/tests/coverage/jest',
  coverageThreshold: {
    // TODO: bring up to 60-80%
    global: {
      branches: 0,
      functions: 0,
      lines: 0,
      statements: 0,
    },
  },
  moduleNameMapper: {
    'data-api-alias': '<rootDir>/src/Foundation/ReactJss/api/JssDataApi',
    'Foundation/(.*)': '<rootDir>/src/Foundation/$1',
    'Project/(.*)': '<rootDir>/src/Project/$1',
    'Feature/(.*)': '<rootDir>/src/Feature/$1',
    '((.(css|scss)$)|(/styles$))': '<rootDir>/scripts/jest/__mocks__/styleMock.js',
  },
  collectCoverageFrom: [
    '**/*.{ts,tsx}',
    '!**/node_modules/**',
    '!**/*.d.ts',
    '!**/actionTypes.ts',
    '!**/Models.Generated.ts',
    '!**/dataProvider/index.ts',
  ],
  transform: {
    '^.+\\.(ts|tsx)$': 'ts-jest',
  },
  testRegex: '(/__tests__/.*|(\\.|/)(test|spec))\\.(jsx?|tsx?)$',
  testResultsProcessor: './scripts/jest/trxProcessor',
  moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx', 'json', 'node'],
  setupFilesAfterEnv: ['./scripts/enzyme/index.ts'],
  snapshotSerializers: ['enzyme-to-json/serializer'],

  // Fixes "SecurityError: localStorage is not available for opaque origins"
  // https://stackoverflow.com/questions/51554366/npm-test-fail-with-jest/51554485
  verbose: true,
  testURL: 'http://localhost/',
};
