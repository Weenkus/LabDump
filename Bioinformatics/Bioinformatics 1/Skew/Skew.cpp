#include <fstream>
#include <iostream>
#include <string>
#include <vector>

std::vector<int> calculateSkew(const std::string& genome);

int main() {
	// Open files
	std::ifstream inputHandle("genome.txt");
	std::ofstream outputHanle("output.txt");

	// Parse
	std::string genome;
	std::getline(inputHandle, genome);

	// Generate output
	std::vector<int> skew = calculateSkew(genome);
	for (const auto& c : skew)
		outputHanle << c << " ";

	// Clean up
	inputHandle.close();
	outputHanle.close();
	return 0;
}

std::vector<int> calculateSkew(const std::string& genome) {
	int genLen = genome.length(), count{ 0 };
	std::vector<int> skew;
	
	skew.push_back(0);
	for (const auto& c : genome) {
		if (c == 'G')
			++count;
		else if(c == 'C')
			--count;
		skew.push_back(count);
	}
	return skew;
}