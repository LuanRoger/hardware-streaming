mod yaml_config;

use kafka::consumer::Consumer;
use yaml_config::yaml_configuration::{self, init_configs};

fn main() {
    init_configs();
}
