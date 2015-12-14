#include <fstream>
#include <iostream>
#include <string>
#include <iostream>

int patternCount(std::string genome, std::string pattern);

int main() {
	
	// Open and read the file
	std::ifstream fileHandle("dataset_2_6.txt");
	std::string genome, pattern;
	
	std::getline(fileHandle, genome);
	std::getline(fileHandle, pattern);
		
	int ptCountS = patternCount(genome, pattern);
	std::cout << "The pattern " << pattern << " is repeted " << ptCountS << " times in the input genome.\n";
	
	getchar();
	return 0;
}

int patternCount(std::string genome, std::string pattern) {
	int genLen = genome.length(), patLen = pattern.length(), count{ 0 };
	for (int i{ 0 }; i < genLen - patLen; i++) {
		if (genome.substr(i, patLen).compare(pattern) == 0 ) {
			++count;
		}
	}
	return count;
}