#include <iostream>
#include <string>
#include <fstream>
#include <istream>
#include <sstream>
#include <vector>
#include <list>

struct Bucket {
	Bucket(int size, int ts) : size(size), timestamp(ts) { }

	int size;
	long long timestamp;
};

bool is_debug_mode{ false };

const int MAX_NUMBER_OF_SAME_SIZE_BUCKETS{ 2 };
const char QUERY_MARK{ 'q' };

long long int TIMESTAMP{ 0 };

void start_stream();
void stream_loop(std::istream& stream);
void process_query(std::list<Bucket>& buckets, const int query_value);
void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size);

inline void print_buckets(std::list<Bucket>& buckets) {
	for (auto b : buckets)
		std::cout << "[" << b.size << ", " << b.timestamp << "] ";
	std::cout << std::endl;
}

inline void delete_old_buckets(std::list<Bucket>& buckets, const int N) {
	for (auto it = buckets.begin(); it != buckets.end(); ) {
		if ((TIMESTAMP - (*it).timestamp) > N) 
			it = buckets.erase(it);
		else 
			++it;
		}
}


inline int get_query_value(const std::string& line) { return std::stoi(line.substr(2)); };

inline bool is_query(const std::string& line) { return line[0] == QUERY_MARK; }



int main() {
	start_stream();
	return 0;
}

void start_stream() {
	std::string stream_buffer;
	if (is_debug_mode) {
		std::ifstream input_handle("../Primjeri/1.in");
		stream_loop(input_handle);
		input_handle.close();
	} else {
		stream_loop(std::cin);
	}
}

void stream_loop(std::istream&  stream){ 
	std::string line;
	std::list<Bucket> buckets;

	std::getline(stream, line);
	int stream_size{ std::stoi(line) };

	while (std::getline(stream, line)) {

		if (is_query(line)) {
			int query_value{ get_query_value(line) };
			process_query(buckets, query_value);
			if (is_debug_mode) getchar();
		} else {
			update_buckets(buckets, line, stream_size);
		}
	
	}
}

void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size) {
	for (auto& stream_element : line) {
		++TIMESTAMP;

		if (stream_element == '1') {
			
			delete_old_buckets(buckets, window_size);
			buckets.emplace_front(1, TIMESTAMP);	

			int current_size{ 1 };
			int num_of_same_buckets{ 1 };

			for (auto it = buckets.begin(); it != buckets.end();) {
				auto bucket = (*it);

				if (bucket.size != current_size) {
					current_size = bucket.size;
					num_of_same_buckets = 1;

				}

				if (num_of_same_buckets <= MAX_NUMBER_OF_SAME_SIZE_BUCKETS) {
					num_of_same_buckets++;
					++it;
				}

				else {
					(*std::prev(it, 1)).size *= 2;
					it = buckets.erase(it);

					num_of_same_buckets = MAX_NUMBER_OF_SAME_SIZE_BUCKETS;
					current_size *= 2;
				}
			}
		}
	}
}

void process_query(std::list<Bucket>& buckets, const int query_value) {
	int total_ones{ 0 };
	int previous_size{ 0 };

	for (auto it = buckets.begin(); it != buckets.end(); ++it) {
		auto bucket = (*it);

		if (bucket.timestamp < TIMESTAMP - query_value) {
			break;
		}

		previous_size = bucket.size;
		total_ones += previous_size;
	}

	if (previous_size != 0) {
		total_ones = total_ones - previous_size + previous_size / 2;
	}

	std::cout <<  total_ones << std::endl;
}
