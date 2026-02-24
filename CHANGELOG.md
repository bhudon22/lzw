# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.1] - 2026-02-23

### Added
- README with usage instructions, algorithm explanation, license, and changelog link
- CHANGELOG.md
- MIT license
- Release notes link to changelog

### Removed
- `.gitignore` and `CLAUDE.md` from version control

## [1.0.0] - 2026-02-23

### Added
- LZW compression and decompression for any Unicode string
- UTF-8 byte encoding so all input fits the standard 256-entry alphabet
- Detection of corrupt compressed data via `InvalidOperationException` for unknown codes that are not the legitimate LZW edge case
