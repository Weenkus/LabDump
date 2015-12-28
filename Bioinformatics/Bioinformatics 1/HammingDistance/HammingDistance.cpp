#include <vector>
#include <iostream>
#include <fstream>
#include <string>

int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {
	// Open file handles
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandel("output.txt");

	// Parse
	std::string genome1, genome2;
	inputHandle >> genome1 >> genome2;

	outputHandel << countMissmatch(genome1, genome2);

	// Clean after yourself
	inputHandle.close();
	outputHandel.close();
	return 0;
}

int countMissmatch(const std::string& genome1, const std::string& genome2) {
	int count{ 0 };
	for (int i{ 0 }; i <  genome1.size(); ++i)
		count += genome1.at(i) != genome2.at(i) ? 1 : 0;
	return count;
}