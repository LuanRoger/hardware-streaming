mod yaml_config;

use core::panic;

use std::{str, convert};

use kafka::{consumer::{Consumer}};
use yaml_config::yaml_configuration::{self, init_configs, YamlConfigModel, KafkaDomainConfigModel};

fn main() {
    let config_result = init_configs();
    let config_match = match config_result {
        Ok(result) => result,
        Err(e) => panic!("Was not possible to read the configuration file: {:?}", e)
    };
    let configuration = config_match;

    let consumer_result = Consumer::from_hosts(vec!["localhost:9092".to_owned()])
    .with_topic(configuration.kafkaConfig.topic).create();
    let consumer_match = match consumer_result {
        Ok(result) => result,
        Err(e) => panic!("A error occurs in consumer: {:?}", e)
    };
    let mut consumer: Consumer = consumer_match;

    loop {
        for message_set in consumer.poll().unwrap().iter() {
            for message in message_set.messages() {
                let message_key = str::from_utf8(message.key).unwrap();
                let message_value: [u8; 4] = [message.value[0], message.value[1], message.value[2], message.value[3]];
                println!("New message: Key [{}]; Value [{}]", message_key, f32::from_le_bytes(message_value));
            }
            _ = consumer.consume_messageset(message_set);
        }
        consumer.commit_consumed().unwrap();
    }
}
