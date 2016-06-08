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

unsigned long long int TIMESTAMP{ 0 };

void start_stream();
void stream_loop(std::istream& stream);
void process_query(std::list<Bucket>& buckets, const int query_value, const int window_size);
void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size);
inline void normalize_timestamps(std::list<Bucket>& buckets, const int window_size);

inline int get_query_value(const std::string& line) { return std::stoi(line.substr(2)); };
inline int get_stream_length(std::stringstream& stream) { return stream.tellp(); };

inline int get_total_size(std::list<Bucket> buckets) { 
	int total_size{ 0 };
	for (const auto& b : buckets)
		total_size += b.size;

	return total_size;
}

inline int get_total_time(std::list<Bucket> buckets, const int window_size) {
	int total_time{ 0 };
	for (auto it = buckets.begin(); it != buckets.end(); ++it) {
		if (it != buckets.begin()) {
			if ((*it).timestamp <= (*std::prev(it, 1)).timestamp)
				total_time += (*std::prev(it, 1)).timestamp - (*it).timestamp;
			else
				total_time += (*std::prev(it, 1)).timestamp + window_size - (*it).timestamp;
		}
	}
	return total_time;
}

inline int get_biggest_bucket_size(std::list<Bucket> buckets) {
	int max = 0;
	for (const auto& b : buckets) {
		if (b.size > max) max = b.size;
	}

	return max;
}

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
	int line_counter{ 0 };
	while (std::getline(stream, line)) {
		++line_counter;
		std::cout << "line: " << line_counter << " " << buckets.size() <<'\n';

		if (is_query(line)) {
			int query_value{ get_query_value(line) };
			process_query(buckets, query_value, stream_size);
		} 

		update_buckets(buckets, line, stream_size);
		getchar();
	}
}

void update_buckets(std::list<Bucket>& buckets, const std::string& line, const int window_size) {
	for (auto& c : line) {

		//std::cout << get_total_time(buckets, window_size) << std::endl;
		//std::cout << buckets.back().timestamp << std::endl;
		if (get_total_time(buckets, window_size) > window_size)
			buckets.pop_back();

		//TIMESTAMP = (TIMESTAMP + 1) % window_size;

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
					//(*it).timestamp = (*it_next).timestamp;
					it = buckets.erase(it_next);
				}
				else {
					++it;
				}
			}
		}
		
		//++TIMESTAMP;
		TIMESTAMP = (TIMESTAMP + 1) % window_size;
	}
}

void process_query(std::list<Bucket>& buckets, const int query_value, const int window_size) {
	//normalize_timestamps(buckets, window_size);
	int num_of_ones{ 0 }, timestep{ 0 }, last_size{ 0 };
	bool wrote_to_output{ false };
	for (auto it = buckets.begin(); it != buckets.end(); ++it) {
		auto b = (*it);
		num_of_ones += b.size;

		if (it != buckets.begin()) {
			if((*it).timestamp < (*std::prev(it, 1)).timestamp)
				timestep += (*std::prev(it, 1)).timestamp - (*it).timestamp;
			else
				timestep += (*std::prev(it, 1)).timestamp + window_size - (*it).timestamp;
			std::cout << "q_v: " << query_value << ", b_t: " << b.timestamp << ", b_s: " << b.size << ", ts: " << timestep;
			std::cout << ", size: " <<num_of_ones << std::endl;
		}

		/*if (query_value <= timestep) {
			num_of_ones += b.size / 2;
			std::cout << "Number of ones: " << num_of_ones;
			break;
		}
		else {
			num_of_ones += b.size;
		}*/
	}



	//if (!wrote_to_output) std::cout << "Number of ones(K): " << num_of_ones - last_size/2 << std::endl;
}

inline void normalize_timestamps(std::list<Bucket>& buckets, const int window_size) {
	int current_timestamp{ 0 };
	for (auto it = buckets.begin(); it != buckets.end(); ++it) {

		if (std::next(it, 1) != buckets.end() && it != buckets.begin()) {
			auto it_next = std::next(it, 1);

			std::cout << "it_n: " << (*it_next).timestamp << ", it: " << (*it).timestamp << std::endl;

			if ((*it_next).timestamp < (*it).timestamp)
				current_timestamp += (*it).timestamp - (*it_next).timestamp;
			else {
				current_timestamp += (*it).timestamp + (window_size - (*it_next).timestamp);
			}
		}

		if (it == buckets.begin())
			(*it).timestamp = 0;
		else 
			(*it).timestamp = current_timestamp;
	}
}