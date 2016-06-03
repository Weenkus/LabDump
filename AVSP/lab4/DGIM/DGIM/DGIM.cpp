#include <iostream>
#include <string>
#include <fstream>
#include <istream>
#include <sstream>
#include <vector>
#include <list>

struct Bucket {
	Bucket(int size, int ts) : size(size), timestamp(ts) { }

	int size, timestamp;
};

bool is_debug_mode{ true };

const int MAX_NUMBER_OF_SAME_SIZE_BUCKETS{ 2 };
const int MAX_STREAM_SIZE{ 10000000 };
const char QUERY_MARK{ 'q' };

int TIMESTAMP{ 0 };

void start_stream();
void stream_loop(std::istream& stream);
void process_query(std::list<Bucket>& buckets, const int query_value);
void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size);

inline void empty_stream(std::stringstream* stream) {
	stream->clear();
	stream->str(std::string());
}

inline int get_query_value(const std::string& line) { return std::stoi(line.substr(2)); };
inline int get_stream_length(std::stringstream& stream) { return stream.tellp(); };

inline int get_total_size(std::list<Bucket> buckets) { 
	int total_size = 0; 
	for (const auto& b : buckets)
		total_size += b.size;

	return total_size;
}

inline int get_biggest_bucket_size(std::list<Bucket> buckets) {
	int max = 0;
	for (const auto& b : buckets) {
		if (b.size > max) max = b.size;
	}

	return max;
}


inline bool is_query(const std::string& line) { return line[0] == QUERY_MARK; }
inline bool is_ready_for_memory_swamp(std::stringstream& stream) { return get_stream_length(stream) > MAX_STREAM_SIZE; };




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
	//std::stringstream local_stream, buffer_stream;
	//std::stringstream* current_stream = &buffer_stream;
	std::list<Bucket> buckets;

	std::getline(stream, line);
	int stream_size{ std::stoi(line) };
	int line_counter{ 0 };
	while (std::getline(stream, line)) {
		++line_counter;
		std::cout << "line: " << line_counter << " " << buckets.size() <<'\n';

		if (is_query(line)) {
			int query_value{ get_query_value(line) };
			process_query(buckets, query_value);
		} 

		update_buckets(buckets, line, stream_size);
		getchar();
		/*buffer_stream << line;

		if (is_ready_for_memory_swamp(buffer_stream)) {
			empty_stream(&local_stream);
			local_stream << buffer_stream.rdbuf();
			current_stream = &local_stream;
			empty_stream(&buffer_stream);
		}

		std::cout << std::endl << (*current_stream).tellp() << std::endl;*/
	}
}

void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size) {
	int zero_counter{ 0 };
	for (auto& c : line) {

		if (get_total_size(buckets) > window_size)
			buckets.pop_back();

		TIMESTAMP = (TIMESTAMP + 1) % window_size;

		if (c == '1') {
			buckets.emplace_front(1, TIMESTAMP);	

			int buckets_total_size{ 0 };
			for (auto it = buckets.begin(); it != buckets.end();) {
				buckets_total_size += (*it).size;

				if (it == buckets.begin() || std::next(it, 1) == buckets.end()) {
					++it;
					continue;
				}

				auto it_next = std::next(it, 1);
				auto it_prev = std::prev(it, 1);


				if ((*it_prev).size == (*it_next).size) {
					(*it).size *= 2;
					(*it).timestamp = (*it_next).timestamp;
					it = buckets.erase(it_next);
				}
				else {
					++it;
				}
			}
			zero_counter = 0;
		}
		else {
			zero_counter++;
		}
	}
}

void process_query(std::list<Bucket>& buckets, const int query_value) {
	int num_of_ones{ 0 }, k{ 0 };
	bool wrote_to_output{ false };
	for (auto &b : buckets) {
		std::cout << "q_v: " << query_value << ", b_t: " << b.timestamp << ", b_s:" << b.size << ", k:" << k << std::endl;

		if (query_value < k + b.size) {
			num_of_ones -= (b.size / 2);
			//std::cout << num_of_ones << std::endl;
			std::cout << "Number of ones: " << num_of_ones << std::endl;
			wrote_to_output = true;
			break;
		} else {
			num_of_ones += b.size;
		}

		k += b.size;
	}

	//const int last_size = get_biggest_bucket_size(buckets);
	//if (!wrote_to_output) std::cout << "Number of ones: " << get_total_size(buckets) - last_size/2 << std::endl;
	//if (!wrote_to_output) std::cout << num_of_ones << std::endl;
}
